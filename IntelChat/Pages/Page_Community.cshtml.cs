using IntelChat.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using ThreadingTask = System.Threading.Tasks.Task;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntelChat.Pages
{
    public class Page_CommunityModel : PageModel
    {
		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; set; }
		private readonly IConfiguration _config;
		public List<CommunityMember> members = new List<CommunityMember>();
		public List<SelectListItem> memberDescriptions = new List<SelectListItem>();

		/// **************************************************************************************
		/// ****************** SQL TABLE / Stored PROCEDURE [dbo].[Read_Community] ***************
		/// **************************************************************************************
		public Page_CommunityModel(IConfiguration config)
		{
			_config = config;
			ReadCommunityMembers();
			memberDescriptions.Add(new SelectListItem("-", "0"));
			foreach (CommunityMember member in members)
			{
				memberDescriptions.Add(new SelectListItem($"{member.username} | POD: {member.pod} | PID: {member.pid} | {member.role}", member.pid.ToString()));
			}
		}

		public void OnGet()
		{
			ReturnUrl = Url.Content("/");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ReturnUrl = Url.Content($"/");
			if (!ModelState.IsValid) { ViewData["Error"] = true; return Page(); }

			if(Int32.Parse(Input.Member) == 0) return LocalRedirect(ReturnUrl);

			CommunityMember member = members.Find(member => member.pid == Int32.Parse(Input.Member)) ?? new CommunityMember();
			
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, member.username));
			claims.Add(new Claim(ClaimTypes.Actor, member.pid.ToString()));
			claims.Add(new Claim(ClaimTypes.Locality, member.pod.ToString()));
			claims.Add(new Claim(ClaimTypes.Role, $"{Convert.ToInt32(member.pod)}-{member.role}"));
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(claimsIdentity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			ReturnUrl = Url.Content($"/?pod={member.pod}&pid={member.pid}");
			return LocalRedirect(ReturnUrl);
		}

		public class InputModel
		{
			[Required]
			public string Member { get; set; }
		}

		public class CommunityMember
		{
			public string username { get; set; }
			public int pod { get; set; }
			public int pid { get; set; }
			public string role { get; set; }
		}

		private SqlDataReader ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQueryAsync();
			return null;
		}

		private void ReadCommunityMembers()
		{
			List<SqlParameter> parameters = new List<SqlParameter>();
			var reader = ExecuteStoredProcedure("dbo.[sp_Community_Bypass]", parameters, true);
			if (reader == null) return;

			members.Clear();
			while (reader.Read())
			{
				members.Add(new CommunityMember
				{
					username = reader.GetString(0),
					pod = reader.GetInt32(1),
					pid = reader.GetInt32(2),
					role = reader.GetString(3)
				});
			}
			reader.Close();
		}
	}
}
