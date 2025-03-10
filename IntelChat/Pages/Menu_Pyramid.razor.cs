using Microsoft.AspNetCore.Components;

namespace IntelChat.Pages
{
	public partial class Menu_Pyramid
	{
		/*
			************************************************************************************
			******************************** INPUT PARAMETERS **********************************
			************************************************************************************
		 */
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? tid { get; set; }
	}
}