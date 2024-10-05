using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Data.SqlClient;

namespace IntelChat.Pages
{
	public partial class Detail_Verb
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
		private List<Verb> verbs = new List<Verb>();
		private Verb currentVerb = new Verb();
		private int verbIndex;

		private void AutoFill()
		{
			Verb target = verbs[verbIndex];
			currentVerb.VerbId = target.VerbId;
			currentVerb.VerbLabel = target.VerbLabel;
			currentVerb.VerbDescription = target.VerbDescription;
			currentVerb.VerbType = target.VerbType;
			currentVerb.VerbStatus = target.VerbStatus;
			currentVerb.UrlIdPk = target.UrlIdPk;
		}

		private void Previous()
		{
			if (verbIndex > 0)
			{
				verbIndex--;
				AutoFill();
			}
		}

		private void Next()
		{
			if (verbIndex < verbs.Count - 1)
			{
				verbIndex++;
				AutoFill();
			}
		}

		//*******************************************************************************
		//**************************** Stored Procedure *********************************
		//****************************    VERB CRUD     *********************************
		//*******************************************************************************

		//private void Read()
		//{
		//	string spName = "dbo.CRUD_Verb";
		//	SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
		//	SqlCommand cmd = new SqlCommand(spName, connection);

		//	cmd.Parameters.Add(new SqlParameter("@PROC_filter", "****"));
		//	cmd.Parameters.Add(new SqlParameter("@PROC_action", "Read"));
		//	cmd.Parameters.Add(new SqlParameter("@pod", pod));

		//	cmd.Parameters.Add(new SqlParameter("@status", "*")); // this for all, without it only will add if status  = A

		//	connection.Open();
		//	cmd.CommandType = CommandType.StoredProcedure;

		//	SqlDataReader reader = cmd.ExecuteReader();
		//	verbs.Clear();
		//	while (reader.Read())
		//	{
		//		verbs.Add(new Verb
		//		{
		//			VerbId = reader.GetInt32(0),
		//			VerbLabel = reader.GetString(1),
		//			VerbDescription = reader.GetString(2),
		//			VerbType = reader.GetString(3),
		//			VerbStatus = reader.GetString(4),
		//			PodIdFk = reader.GetInt32(5),
		//			UrlIdPk = reader.GetInt32(6)
		//		});
		//	}

		//	verbs = verbs.OrderBy(verb => verb.VerbId).ToList();
		//}

		//********************** Override the OnInitialized method ********************
		//********************** ReRead lists when necessary ********************

		protected override void OnInitialized()
		{
			//Read();
			verbs = verbService.Verbs;
			verbIndex = verbs.FindIndex(x => x.VerbId == id);
			AutoFill();
		}
	}
}