using IntelChat.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using static Azure.Core.HttpHeader;
using System;

namespace IntelChat.Pages
{
    public class RecoveryModel : PageModel
    {
		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; set; }

		private readonly IConfiguration _config;

		private readonly IHttpContextAccessor _context;

		public RecoveryModel(IConfiguration config, IHttpContextAccessor context)
		{
			_config = config;
			_context = context;

		}

		public void OnGet()
		{
			ReturnUrl = Url.Content("~/");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ReturnUrl = Url.Content("/Login");
			if (!ModelState.IsValid) return Page();

			if (Input.NewPassword != Input.ConfirmPassword) return Page();

			PasswordHasher<string> hasher = new PasswordHasher<string>();
			var hash = _context.HttpContext?.User.FindFirst(ClaimTypes.Authentication);
			if (hash == null) return Page();
			PasswordVerificationResult authenticated = hasher.VerifyHashedPassword(Input.Email, hash.Value, Input.Code);
			if (!authenticated.HasFlag(PasswordVerificationResult.Success)) return Page();

			Registration? registration = ReadRegistration("email", Input.Email);
			if (registration == null) return Page();
			var newHash = hasher.HashPassword(Input.Username, Input.NewPassword);
			UpdateRegistration(registration, Input.Username, newHash);

			await HttpContext.SignOutAsync();
			return LocalRedirect(ReturnUrl);
		}

		public class InputModel
		{
			[Required]
			public string Pod { get; set; }

			[Required]
			[DataType(DataType.EmailAddress)]
			public string Email { get; set; }

			[Required]
			public string Code { get; set; }

			[Required]
			public string Username { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string NewPassword { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string ConfirmPassword { get; set; }
		}

		public void UpdateRegistration(Registration registration, string username, string password)
		{
			string spName = "dbo.[CRUD_Registration]";

			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@id", registration.RegistrationId),
				new SqlParameter("@username", username),
				new SqlParameter("@password", password),
				new SqlParameter("@email", registration.RegistrationEmail),
				new SqlParameter("@status", registration.RegistrationStatus),
				new SqlParameter("@person_id_fk", registration.PersonIdFk)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.ExecuteNonQuery();
			connection.Close();
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
