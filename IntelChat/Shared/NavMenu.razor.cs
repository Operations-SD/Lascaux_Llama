using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IntelChat.Shared
{
	public partial class NavMenu
	{
		private bool collapseNavMenu = true;
		private int pod = 3;
		private int pid = 0;
		private string name = "";

		private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private void ToggleNavMenu()
		{
			collapseNavMenu = !collapseNavMenu;
		}

		private string getHref(string link)
		{
			return $"{link}?pod={pod}&pid={pid}";
		}

		private string getRole(string role)
		{
			return $"{pod}-{role}";
		}
		protected override void OnInitialized()
		{
            var names = context.HttpContext?.User.FindAll(ClaimTypes.Name);
            var actors = context.HttpContext?.User.FindAll(ClaimTypes.Actor);
			var localities = context.HttpContext?.User.FindAll(ClaimTypes.Locality);
			name = (names.IsNullOrEmpty() == true) ? "" : names.First().Value;
			pod = (actors.IsNullOrEmpty() == true) ? 0 : Int32.Parse(localities.First().Value);
			pid = (actors.IsNullOrEmpty() == true) ? 0 : Int32.Parse(actors.First().Value);
        }
	}
}