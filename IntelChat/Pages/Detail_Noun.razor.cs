using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Data.SqlClient;

namespace IntelChat.Pages
{
	public partial class Detail_Noun
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? id { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public string? prevPage { get; set; }
		private List<Noun> nouns = new List<Noun>();
		private Noun currentNoun = new Noun();
		private int nounIndex;

		private void AutoFill()
		{
			Noun target = nouns[nounIndex];
			currentNoun.NounId = target.NounId;
			currentNoun.NounLabel = target.NounLabel;
			currentNoun.NounDescription = target.NounDescription;
			currentNoun.NounType = target.NounType;
			currentNoun.NounStatus = target.NounStatus;
			currentNoun.UrlIdPk = target.UrlIdPk;
		}

		private void Previous()
		{
			if (nounIndex > 0)
			{
				nounIndex--;
				AutoFill();
			}
		}

		private void Next()
		{
			if (nounIndex < nouns.Count - 1)
			{
				nounIndex++;
				AutoFill();
			}
		}

		protected override void OnInitialized()
		{
			//Read();
			nouns = nounService.Nouns;
			nounIndex = nouns.FindIndex(x => x.NounId == id);
			AutoFill();
		}
	}
}