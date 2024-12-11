using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Data;
using IntelChat.Models;
using System.Data.SqlClient;
using Registration = IntelChat.Models.Registration;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting.Server;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Security.Cryptography;

namespace IntelChat.Pages
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; set; }

		private readonly IConfiguration _config;
		public LoginModel(IConfiguration config)
		{
			_config = config;
		}

		public void OnGet()
		{
			ReturnUrl = Url.Content("~/");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ReturnUrl = Url.Content("~/");
			if (!ModelState.IsValid) { ViewData["Error"] = true; return Page(); }

			Registration? registration = ReadRegistration("username", Input.Username);
			if (registration == null) { ViewData["Error"] = true; return Page(); }

			RegistrationRole? registrationRole = ReadRegistrationRole(registration.RegistrationId);
			if (registrationRole == null) { ViewData["Error"] = true; return Page(); }

			PasswordHasher<string> hasher = new PasswordHasher<string>();
			var authenticated = hasher.VerifyHashedPassword(Input.Username, registration.RegistrationPassword, Input.Password);
			if (!authenticated.HasFlag(PasswordVerificationResult.Success)) { ViewData["Error"] = true; return Page(); }
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, Input.Username));
			claims.Add(new Claim(ClaimTypes.Actor, registration.PersonIdFk.ToString()));
			claims.Add(new Claim(ClaimTypes.Email, registration.RegistrationEmail.ToString()));
			claims.Add(new Claim(ClaimTypes.Locality, Input.Pod));
			claims.Add(new Claim(ClaimTypes.Role, $"{Convert.ToInt32(Input.Pod)}-{registrationRole.PersonPypeDdRole}"));
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
			public string Username { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }
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
		private RegistrationRole? ReadRegistrationRole(int registrationId = 0, string username = "")
		{
			string spName = "dbo.[Registration_Role_Read]";
			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@id", registrationId),
				new SqlParameter("@username", username)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.Read())
			{
				RegistrationRole registrationRole = new RegistrationRole()
				{
					RegistrationId = reader.GetInt32(0),
					RegistrationUsername = reader.GetString(1),
					PersonPypeDdMyme = reader.GetString(2),
					PersonPypeDdRole = reader.GetString(3)
				};
				connection.Close();
				return registrationRole;
			}
			else
			{
				connection.Close();
				return null;
			}
		}
	}
}
