using IntelChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Data.SqlClient;
using Registration = IntelChat.Models.Registration;
using Microsoft.AspNetCore.Identity;
using System;

namespace IntelChat.Pages
{
    public class RecoveryEmailModel : PageModel
    {
		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; set; }

		private readonly IConfiguration _config;
		public RecoveryEmailModel(IConfiguration config)
		{
			_config = config;
		}

		public void OnGet()
		{
			ReturnUrl = Url.Content("~/");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ReturnUrl = Url.Content("/Recovery");
			if (!ModelState.IsValid) return Page();

			Registration? registration = ReadRegistration("email", Input.Email);
			if (registration == null) return Page();

			var smtp = new SMTPConfig
			{
				Username = _config.GetValue<string>("SMTP:Username"),
				Password = _config.GetValue<string>("SMTP:Password"),
				Host = _config.GetValue<string>("SMTP:Host"),
				Port = _config.GetValue<string>("SMTP:Port")
			};

			string code = RandomNumberGenerator.GetHexString(8, true);
			SendEmail(smtp,
				registration.RegistrationEmail,
				"Account Recovery",
				$"{code} is your Intel-a-Chat recovery code");

			PasswordHasher<string> hasher = new PasswordHasher<string>();
			string hash = hasher.HashPassword(Input.Email, code);
			PasswordVerificationResult authenticated = hasher.VerifyHashedPassword(Input.Email, hash, code);
			if (!authenticated.HasFlag(PasswordVerificationResult.Success)) return Page();
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Authentication, hash));
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(claimsIdentity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			return LocalRedirect(ReturnUrl);
		}

		public class InputModel
		{
			[Required]
			public string Pod { get; set; }

			[Required]
			[DataType(DataType.EmailAddress)]
			public string Email { get; set; }
		}

		private class SMTPConfig
		{
			public string Username { get; set; }
			public string Password { get; set; }
			public string Host { get; set; }
			public string Port { get; set; }
		}

		private bool SendEmail(SMTPConfig config, string to, string subject, string body)
		{
			// Create message with from, to, subject, and body
			MailMessage message = new MailMessage(config.Username, to, subject, body);

			// Create the SMTP client that will send the message
			SmtpClient client = new SmtpClient
			{
				Host = config.Host,
				Port = Int32.Parse(config.Port),
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(config.Username, config.Password),
				EnableSsl = true
			};

			// Send message to recipient
			try { client.Send(message); return true; }
			catch (Exception ignored) { return false; }
		}

		private Registration? ReadRegistration(string keyType, string key)
		{
			string spName = "dbo.[CRUD_Registration]";

			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter($"@{keyType}", key)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.Read())
			{
				Registration registration = new Registration()
				{
					RegistrationId = reader.GetInt32(0),
					RegistrationUsername = reader.GetString(1),
					RegistrationPassword = reader.GetString(2),
					RegistrationEmail = reader.GetString(3),
					RegistrationStatus = reader.GetString(4),
					PersonIdFk = reader.GetInt32(5)
				};
				connection.Close();
				return registration;
			}
			else
			{
				connection.Close();
				return null;
			}
		}
	}
}
