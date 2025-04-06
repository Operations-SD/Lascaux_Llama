using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Components;
using IntelChat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using IntelChat.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace IntelChat.Pages
{
	public class RegisterModel : PageModel
	{
		[BindProperty]
		public InputModel Input { get; set; }
		public readonly NotificationService _notification;
		public string ReturnUrl { get; set; }
		private readonly IConfiguration _config;
		public List<SelectListItem> Types = new List<SelectListItem>();
		private List<Pype> pypes = new List<Pype>();
		private List<Execute> execute = new List<Execute>();

		public RegisterModel(IConfiguration config)
		{
			_config = config;
			ReadPype("PRSN", 3);
			pypes.ForEach(pype => Types.Add(new SelectListItem { Text = pype.PypeLabel, Value = pype.PypeType }));
		}

		public void OnGet()
		{
			ReturnUrl = Url.Content("~/");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ReturnUrl = Url.Content("~/");
			if (!ModelState.IsValid) { ViewData["Error"] = "Some fields have not been filled!"; return Page(); }

			// Check if password == confirm password
			if (Input.Password != Input.Confirm) { ViewData["Error"] = "Confirm Password is not the same as Password!"; return Page(); }

			// Check if given brand code exists and is usable
			Brand? brand = ReadBrand(Input.Code);
			if (brand == null) 
				{ ViewData["Error"] = "Brand code could not be found!"; return Page(); }
			if (brand.BrandStatus != "A")
				{ ViewData["Error"] = "Brand status is inactive!"; return Page(); }
			if (brand.BrandRegDateClosed <= DateTime.Now)
				{ ViewData["Error"] = "Brand registration has expired!"; return Page(); }
			if (brand.BrandCntMax <= brand.BrandCntReg)
				{ ViewData["Error"] = "Maximum registration for this brand has been reached!"; return Page(); }

			// Check if an account with the given username already exists
			Registration? registration = ReadRegistration("username", Input.Username);
			if (registration != null) { ViewData["Error"] = "An account with this username already exists!"; return Page(); }

			// Check if an account with the given email address already exists
			registration = ReadRegistration("email", Input.Email);
			if (registration != null) { ViewData["Error"] = "An account with this email already exists!"; return Page(); }

			// Create new person based on the given information
			var type = Input.Type.IsNullOrEmpty() ? "NONE" : Input.Type;
			CreatePerson(Input.Fname, Input.Lname, "NONE", type, "A", brand.BrandRole, DateTime.Now, Convert.ToInt32(Input.Pod), brand.LocationIdFk, brand.ProgramIdFk);
			Person? person = ReadPerson(Input.Fname, Input.Lname);
			if (person == null) { ViewData["Error"] = "Person could not be created!"; return Page(); }

			// Create a new registration for the person that was created
			PasswordHasher<string> hasher = new PasswordHasher<string>();
			string hash = hasher.HashPassword(Input.Username, Input.Password);
			CreateRegistration(Input.Username, hash, Input.Email, person.PersonId);
			registration = ReadRegistration("username", Input.Username);
			if (registration == null) { ViewData["Error"] = "Registration could not be created!"; return Page(); }
			IncrementBrand(brand.BrandId);

			// Log into the new registration and establish its roles
			PasswordVerificationResult authenticated = hasher.VerifyHashedPassword(Input.Username, registration.RegistrationPassword, Input.Password);
			if (!authenticated.HasFlag(PasswordVerificationResult.Success)) { ViewData["Error"] = "Password could not be validated!"; return Page(); }
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, Input.Username));
			claims.Add(new Claim(ClaimTypes.Actor, person.PersonId.ToString()));
			claims.Add(new Claim(ClaimTypes.Email, registration.RegistrationEmail.ToString()));
			claims.Add(new Claim(ClaimTypes.Locality, Input.Pod));
			claims.Add(new Claim(ClaimTypes.Role, $"{Convert.ToInt32(Input.Pod)}-{brand.BrandRole}"));
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(claimsIdentity);

			Execute_Table(person.PersonId, person.PodIdFk);


			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			return LocalRedirect(ReturnUrl);
		}

		public class InputModel
		{
			[Required]
			public string Pod { get; set; }

			[Required]
			public string Code { get; set; }

			[Required]
			public string Username { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string Confirm { get; set; }

			[Required]
			[DataType(DataType.EmailAddress)]
			public string Email { get; set; }

			[ValidateNever]
			public string Fname { get; set; } = "";

			[ValidateNever]
			public string Lname { get; set; } = "";

			[ValidateNever]
			public string Type { get; set; } = "";
		}
		public void CreateRegistration(string username, string password, string email, int personId)
		{
			string spName = "dbo.[CRUD_Registration]";

			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Create"),
				new SqlParameter("@username", username),
				new SqlParameter("@password", password),
				new SqlParameter("@email", email),
				new SqlParameter("@person_id_fk", personId)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = cmd.ExecuteReader();
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

		private Brand? ReadBrand(string code)
		{
			string spName = "dbo.[CRUD_Brand]";
			using (SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection")))
			using (SqlCommand cmd = new SqlCommand(spName, connection))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@PROC_Action", "Register");
				cmd.Parameters.AddWithValue("@brand_code", code);
				cmd.Parameters.AddWithValue("@brand_status", "*");

				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						return new Brand()
						{
							BrandId = reader.GetInt32(0),
							BrandCode = reader.GetString(1),
							BrandLabel = reader.GetString(2),
							BrandStatus = reader.GetString(3),
							BrandCntMax = reader.GetInt16(4),
							BrandCntReg = reader.GetInt16(5),
							BrandRegDateClosed = reader.GetDateTime(6),
							BrandRole = reader.GetString(7),
							BrandEligibility = reader.GetInt16(8),
							BrandCost = reader.GetDecimal(9),
							LocationIdFk = reader.GetInt32(10),
							ProgramIdFk = reader.GetInt32(11),
							GuideIdFk = reader.GetInt32(12),
							NovaIdFk = reader.GetInt32(13)
						};
					}
				}
			}
			return null;
		}

		private void IncrementBrand(int id)
		{
			string spName = "dbo.[New_Brand_Registration]";
			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@id", id)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.ExecuteNonQuery();
		}

		private Person? ReadPerson(string first, string last)
		{
			string spName = "dbo.[CRUD_Person]";
			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@first", first),
				new SqlParameter("@last", last)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = cmd.ExecuteReader();

			Person? person = null;
			while (reader.Read())
			{
				// Retrieves the newest person with the given first and last name
				person = new Person()
				{
					PersonId = reader.GetInt32(0),
					PersonFirst = reader.GetString(1),
					PersonLast = reader.GetString(2),
					PersonLabel = reader.GetString(3),
					PersonPypeDdMyme = reader.GetString(4),
					PersonStatus = reader.GetString(5),
					PersonPypeDdRole = reader.GetString(6),
					PersonDatetime = reader.GetDateTime(7),
					PodIdFk = reader.GetInt32(8),
					LocationIdFk = reader.GetInt32(9)
				};
			}
			connection.Close();
			return person;
		}

		private void CreatePerson(String first, String last, String label, String type,
			String status, String role, DateTime dateTime, int podIdFk, int locationIdFk, int programIdFk)
		{
			string spName = "dbo.[CRUD_Person]";
			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Create"),
				new SqlParameter("@first", first),
				new SqlParameter("@last", last),
				new SqlParameter("@label", label),
				new SqlParameter("@type", type),
				new SqlParameter("@person_status", status),
				new SqlParameter("@role", role),
				new SqlParameter("@date_time", dateTime),
				new SqlParameter("@pod", podIdFk),
				new SqlParameter("@location_id_fk", locationIdFk),
				new SqlParameter("@program_id_fk", programIdFk)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		private void ReadPype(string filter, int pod)
		{
			string spName = "dbo.[sp_Pype_Type_Locked]";
			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_Input_Filter", filter),
				new SqlParameter("@pod", pod)
			};
			parameters.ForEach(parameter => cmd.Parameters.Add(parameter));

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				pypes.Add(new Pype
				{
					PypeId = reader.GetString(0),
					PypeType = reader.GetString(1),
					PypeLabel = reader.GetString(2),
					PypeStatus = reader.GetString(3),
					PypeDesc = reader.GetString(4),
					PypeLink = reader.GetString(5),
				});
			}
			connection.Close();
		}

		private SqlDataReader? ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQuery();
			return null;
		}
		private void CreateMemo(int pid, int pod, string role, string message)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Create"),
				new SqlParameter("@memo_person_to",  ReadPodRolePerson(pid, pod, role)),
				new SqlParameter("@memo_person_from", pid),
				new SqlParameter("@memo_date_time", DateTime.Now),
				new SqlParameter("@memo_priority", 0),
				new SqlParameter("@memo_pod", pod),
				new SqlParameter("@memo_nova", 0),
				new SqlParameter("@memo_channel", 0),
				new SqlParameter("@memo_type", "COMM"),
				new SqlParameter("@memo_status", "A"),
				new SqlParameter("@memo_message", message)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters);
		}

		private int ReadPodRolePerson(int? pid, int pod, string role)
		{
			if (role == "") return pid ?? 0;
			if (role == "self") return pid ?? 0;

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@role", role)
			};
			var reader = ExecuteStoredProcedure("dbo.[Read_POD_Role_Person]", parameters, true);
			if (reader == null) return 0;
			reader.Read();
			int recipientId = reader.GetInt32(0);
			reader.Close();
			return recipientId;
		}

		private void Execute_Table(int pid, int pod)
        {
            string spName = "dbo.[Read_Execute]";
            using SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
            using SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                execute.Add(new Execute
				{
					ExecId = reader.GetInt32(0),
					WorkTypeStatusE = reader.GetString(1),
					ExecuteText = reader.GetString(2),
					GuideIdFk = reader.GetInt32(3),
					ExecuteR = reader.GetByte(4),
					ExecuteS = reader.GetByte(5),
					Question = reader.GetInt32(6),
					Role = reader.GetString(7)
				});     
            }
            connection.Close();

			//while loop here
			int execIndex = 0;
			while (execIndex < execute.Count)
			{
				Execute currentExec = execute[execIndex];

				switch (currentExec.WorkTypeStatusE)
				{
					case "MEMO":
						CreateMemo(pid, pod, currentExec.Role, currentExec.ExecuteText);
						break;
					case "Gadd":

						break;
					case "QARS":

						break;
				}
				execIndex++;
			}
        }

    }
}
