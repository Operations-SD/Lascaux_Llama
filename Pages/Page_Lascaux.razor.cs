using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	/// *********************************************************************************
	/// *****************  Input ???? Parameters from Source Page Nav_to ****************
	/// *********************************************************************************
	public partial class Page_Lascaux
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? subid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? actid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? objid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public string? prevPage { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public string? type { get; set; } // *********** value from other page??? ************* Lascaux Case switch *********************


		private List<Lascaux> novas = new List<Lascaux>();
		public  Lascaux novaLasc = new Lascaux();


		/// ****************************************************************************************
		/// *****************  Input LASCAUX - NOVA LIST  (not NULLS?) ******* MODEL ***************
		/// ****************************************************************************************
		public class Lascaux
		{
			public int NovaId { get; set; }
			public string ? NovaDescription { get; set; }
			public string ? NovaSubjectLabel { get; set; }
			public string ? NovaActionLabel { get; set; }
			public string ? NovaObjectLabel { get; set; }
			public string ? NovaSubjectDescription { get; set; }
			public string ? NovaActionDescription { get; set; }
			public string ? NovaObjectDescription { get; set; }
			public string ? SubjectURL { get; set; }
			public string ? ActionURL { get; set; }
			public string ? ObjectURL { get; set; }
		}

		/// **************************************************** select NOVA to display from LIST ***********************
		private int _selectedId = 1; // user filter selection
		public int selectedId
		{
			get { return _selectedId; }
			set
			{
				if (_selectedId != value)
				{
					_selectedId = value;
					setNovaLasc();
				}
			}
		}
		/// ******************************************** FIND NOVA to display from LIST ??? POD=3 *********
		private void setNovaLasc()
		{
			novaLasc = novas.Find(x => x.NovaId == selectedId);
		}


		  
		/// *********************************************************************************
		/// ******************* Display Next / Previous NOVA ********************************
		/// *********************************************************************************
		private void GoToNextNova()
		{
			int curIndex = novas.FindIndex(x => x == novaLasc);
			if (curIndex < novas.Count() - 1)
			{
				selectedId = novas[curIndex + 1].NovaId;
			}
		}
		private void GoToPreviousNova()
		{
			int curIndex = novas.FindIndex(x => x == novaLasc);
			if (curIndex > 0)
			{
				selectedId = novas[curIndex - 1].NovaId;
			}
		}
		 


		//*******************************************************************************
		//**************************** Stored Procedure *********************************
		//****************************    SQL connect   *********************************
		//*******************************************************************************

		private async Task<SqlDataReader?> ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			await connection.OpenAsync();
			if (reader) return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
			await command.ExecuteNonQueryAsync();
			return null;
		}

		private SqlDataReader? ExecuteStoredProcedure2(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQueryAsync();
			return null;
		}

		/// *********************************************************************************
		/// *********************** filter "****" get all records *************************** ????????
		/// *********************************************************************************
		private async Task<SqlDataReader?> Read(string sp, string filter = "****")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_Input_Filter", filter)
			};
			return await ExecuteStoredProcedure($"dbo.[{sp}]", parameters, true);
		}

		private SqlDataReader? CreateTempTable(string sp, string status = "*")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_Input_Filter", "****"),
			};
			return ExecuteStoredProcedure2($"dbo.[{sp}]", parameters, true);
		}


		/// ************************ LOAD INTERVIEW *****************************************
		/// ************************ LOAD INTERVIEW *****************************************
		/// ************************ LOAD INTERVIEW *****************************************
		private async ThreadingTask LoadInterview(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryInterview> entities = new List<XnovaDictionaryInterview>();
			while (await reader.ReadAsync())
			{	
				entities.Add(new XnovaDictionaryInterview
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					G = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					Q = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
					QuestionText = !reader.IsDBNull(4) ? reader.GetString(4) : "",
					N = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
					NovaDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					S = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
					Subject = !reader.IsDBNull(8) ? reader.GetString(8) : "",
					SubjectDescription = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					SubjectUrl = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					V = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0,
					Verb = !reader.IsDBNull(12) ? reader.GetString(12) : "",
					VerbDescription = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					VerbUrl = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					O = !reader.IsDBNull(15) ? reader.GetInt32(15) : 0,
					Object = !reader.IsDBNull(16) ? reader.GetString(16) : "",
					ObjectDescription = !reader.IsDBNull(17) ? reader.GetString(17) : "",
					ObjectUrl = !reader.IsDBNull(18) ? reader.GetString(18) : "",
					Pid = !reader.IsDBNull(19) ? reader.GetInt32(19) : 0
				});
			}
			reader.Close();

			entities = entities.FindAll(entity => pid == entity.Pid);
			novas.Clear();
			foreach (XnovaDictionaryInterview entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N ?? 0,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Verb ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.VerbDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.VerbUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}


		/// ************************ LOAD NOUN OBJECT +**************************************
		/// ************************ LOAD NOUN OBJECT ***************************************
		/// ************************ LOAD NOUN OBJRCT ***************************************
		private async ThreadingTask LoadNounObject(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryNounObject> entities = new List<XnovaDictionaryNounObject>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryNounObject
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					N = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					NovaDescription = !reader.IsDBNull(3) ? reader.GetString(3) : "",
					S = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
					Subject = !reader.IsDBNull(5) ? reader.GetString(5) : "",
					SubjectDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					SubjectUrl = !reader.IsDBNull(7) ? reader.GetString(7) : "",
					A = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
					Action = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					ActionDescription = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					ActionUrl = !reader.IsDBNull(11) ? reader.GetString(11) : "",
					O = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
					Object = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					ObjectDescription = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					ObjectUrl = !reader.IsDBNull(15) ? reader.GetString(15) : "",
				});
			}
			reader.Close();

			novas.Clear();
			foreach (XnovaDictionaryNounObject entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N ?? 0,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Action ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.ActionDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.ActionUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}


		/// ************************ LOAD NOUN SUBJECT **************************************
		/// ************************ LOAD NOUN SUBJECT **************************************
		/// ************************ LOAD NOUN SUBJECT **************************************
		private async ThreadingTask LoadNounSubject(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryNounSubject> entities = new List<XnovaDictionaryNounSubject>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryNounSubject
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					N = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					NovaDescription = !reader.IsDBNull(3) ? reader.GetString(3) : "",
					S = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
					Subject = !reader.IsDBNull(5) ? reader.GetString(5) : "",
					SubjectDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					SubjectUrl = !reader.IsDBNull(7) ? reader.GetString(7) : "",
					A = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
					Action = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					ActionDescription = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					ActionUrl = !reader.IsDBNull(11) ? reader.GetString(11) : "",
					O = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
					Object = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					ObjectDescription = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					ObjectUrl = !reader.IsDBNull(15) ? reader.GetString(15) : "",
				});
			}
			reader.Close();

			novas.Clear();
			foreach (XnovaDictionaryNounSubject entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N ?? 0,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Action ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.ActionDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.ActionUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}


		/// ************************ LOAD NOVA **********************************************
		/// ************************ LOAD NOVA ********************************************** used from Page_Nova
		/// ************************ LOAD NOVA **********************************************

		private void LoadNOVA1(string sp, string status = "*")
		{
			var reader = CreateTempTable(sp, status);
			if (reader == null) return;

			novas.Clear();
			while (reader.Read())
			{
				novas.Add(new Lascaux
				{
					NovaId = reader.GetInt32(2),
					NovaDescription = reader.GetString(3),
					NovaSubjectLabel = reader.GetString(5),
					NovaActionLabel = reader.GetString(9),
					NovaObjectLabel = reader.GetString(13),
					NovaSubjectDescription = reader.GetString(6),
					NovaActionDescription = reader.GetString(10),
					NovaObjectDescription = reader.GetString(14),
					SubjectURL = reader.GetString(7),
					ActionURL = reader.GetString(11),
					ObjectURL = reader.GetString(15)
				});
			}

			reader.Close();
		}

		/*
		private async ThreadingTask LoadNOVA(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryNova> entities = new List<XnovaDictionaryNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryNova
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					N = 20,  //!reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					NovaDescription = !reader.IsDBNull(3) ? reader.GetString(3) : "",
					S = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
					Subject = !reader.IsDBNull(5) ? reader.GetString(5) : "",
					SubjectDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					SubjectUrl = !reader.IsDBNull(7) ? reader.GetString(7) : "",
					A = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
					Action = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					ActionDescription = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					ActionUrl = !reader.IsDBNull(11) ? reader.GetString(11) : "",
					O = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
					Object = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					ObjectDescription = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					ObjectUrl = !reader.IsDBNull(15) ? reader.GetString(15) : "",
					Pid = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0
				});
			}
			reader.Close();
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (XnovaDictionaryNova entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Action ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.ActionDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.ActionUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}

		*/


		/// ************************ LOAD POD **********************************************
		/// ************************ LOAD POD ********************************************** used from Page_Nova
		/// ************************ LOAD POD **********************************************
		private async ThreadingTask LoadPOD(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryPod> entities = new List<XnovaDictionaryPod>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryPod
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					N = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					NovaDescription = !reader.IsDBNull(3) ? reader.GetString(3) : "",
					S = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
					Subject = !reader.IsDBNull(5) ? reader.GetString(5) : "",
					SubjectDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					SubjectUrl = !reader.IsDBNull(7) ? reader.GetString(7) : "",
					A = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
					Action = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					ActionDescription = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					ActionUrl = !reader.IsDBNull(11) ? reader.GetString(11) : "",
					O = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
					Object = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					ObjectDescription = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					ObjectUrl = !reader.IsDBNull(15) ? reader.GetString(15) : "",
					Pid = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0
				});
			}
			reader.Close();

			novas.Clear();
			foreach (XnovaDictionaryPod entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Action ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.ActionDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.ActionUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}



		/// ************************ LOAD Question **********************************************
		/// ************************ LOAD Question **********************************************
		/// ************************ LOAD Question **********************************************
		private async ThreadingTask LoadQuestion(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryQuestion> entities = new List<XnovaDictionaryQuestion>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryQuestion
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					N = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					NovaDescription = !reader.IsDBNull(3) ? reader.GetString(3) : "",
					S = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
					Subject = !reader.IsDBNull(5) ? reader.GetString(5) : "",
					SubjectDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					SubjectUrl = !reader.IsDBNull(7) ? reader.GetString(7) : "",
					A = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
					Action = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					ActionDescription = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					ActionUrl = !reader.IsDBNull(11) ? reader.GetString(11) : "",
					O = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
					Object = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					ObjectDescription = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					ObjectUrl = !reader.IsDBNull(15) ? reader.GetString(15) : "",
					Pid = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0
				});
			}
			reader.Close();

			novas.Clear();
			foreach (XnovaDictionaryQuestion entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Action ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.ActionDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.ActionUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}



		/// ************************ LOAD VERB **********************************************
		/// ************************ LOAD VERB **********************************************
		/// ************************ LOAD VERB **********************************************
	


		/// **************** CASE ********************** Internal Procedure LoadTask **************
		/// **************** CASE ********************** Internal Procedure LoadTask **************
		/// **************** CASE ********************** Internal Procedure LoadTask **************
		private async ThreadingTask LoadTask(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryInterview> entities = new List<XnovaDictionaryInterview>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryInterview
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					G = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					Q = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
					QuestionText = !reader.IsDBNull(4) ? reader.GetString(4) : "",
					N = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
					NovaDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					S = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
					Subject = !reader.IsDBNull(8) ? reader.GetString(8) : "",
					SubjectDescription = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					SubjectUrl = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					V = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0,
					Verb = !reader.IsDBNull(12) ? reader.GetString(12) : "",
					VerbDescription = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					VerbUrl = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					O = !reader.IsDBNull(15) ? reader.GetInt32(15) : 0,
					Object = !reader.IsDBNull(16) ? reader.GetString(16) : "",
					ObjectDescription = !reader.IsDBNull(17) ? reader.GetString(17) : "",
					ObjectUrl = !reader.IsDBNull(18) ? reader.GetString(18) : "",
					Pid = !reader.IsDBNull(19) ? reader.GetInt32(19) : 0
				});
			}
			reader.Close();

			entities = entities.FindAll(entity => pid == entity.Pid);
			novas.Clear();
			foreach (XnovaDictionaryInterview entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N ?? 0,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Verb ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.VerbDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.VerbUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}


		/// ************************ LOAD Work *****************************************
		/// ************************ LOAD Work *****************************************
		/// ************************ LOAD Work *****************************************
		private async ThreadingTask LoadWork(string sp, string filter = "****")
		{
			var reader = await Read(sp);
			if (reader == null) return;

			List<XnovaDictionaryWork> entities = new List<XnovaDictionaryWork>();
			while (await reader.ReadAsync())
			{
				entities.Add(new XnovaDictionaryWork
				{
					About = !reader.IsDBNull(0) ? reader.GetString(0) : "",
					P = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					N = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					NovaDescription = !reader.IsDBNull(3) ? reader.GetString(3) : "",
					S = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
					Subject = !reader.IsDBNull(5) ? reader.GetString(5) : "",
					SubjectDescription = !reader.IsDBNull(6) ? reader.GetString(6) : "",
					SubjectUrl = !reader.IsDBNull(7) ? reader.GetString(7) : "",
					A = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
					Action = !reader.IsDBNull(9) ? reader.GetString(9) : "",
					ActionDescription = !reader.IsDBNull(10) ? reader.GetString(10) : "",
					ActionUrl = !reader.IsDBNull(11) ? reader.GetString(11) : "",
					O = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
					Object = !reader.IsDBNull(13) ? reader.GetString(13) : "",
					ObjectDescription = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					ObjectUrl = !reader.IsDBNull(15) ? reader.GetString(15) : "",
					Pid = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0
				});
			}
			reader.Close();

			novas.Clear();
			foreach (XnovaDictionaryWork entity in entities)
			{
				novas.Add(new Lascaux
				{
					NovaId = entity.N,
					NovaDescription = entity.NovaDescription ?? "",
					NovaSubjectLabel = entity.Subject ?? "",
					NovaActionLabel = entity.Action ?? "",
					NovaObjectLabel = entity.Object ?? "",
					NovaSubjectDescription = entity.SubjectDescription ?? "",
					NovaActionDescription = entity.ActionDescription ?? "",
					NovaObjectDescription = entity.ObjectDescription ?? "",
					SubjectURL = entity.SubjectUrl ?? "",
					ActionURL = entity.ActionUrl ?? "",
					ObjectURL = entity.ObjectUrl ?? ""
				});
			}

			reader.Close();
		}


		//**********************************************************************************************
		//************ CASE LOGIC - using input parameter "type" & internal sp "Loadxxxx ***************
		//**********************************************************************************************
		protected async override ThreadingTask OnInitializedAsync()
		{
			string sp = "";
			switch (type)
			{
				case "POD":
					sp = "sp_nova_Dictionary_POD";
					await LoadPOD(sp);
					break;
				case "NOVA":
					sp = "sp_Read_temp";
					LoadNOVA1(sp);
					break;

				case "NounSubject":
					sp = "sp_nova_Dictionary_Noun_Subject";
					await LoadNounSubject(sp);
					break;
				
				case "NounObject":
					sp = "sp_nova_Dictionary_Noun_Object";
					await LoadNounObject(sp);
					break;

				case "Interview":
					sp = "sp_nova_Dictionary_Interview";
					await LoadInterview(sp);
					break;
				case "Question":
					sp = "sp_nova_Dictionary_Question";
					await LoadQuestion(sp);
					break;

				case "Task":
					sp = "sp_nova_Dictionary_Task";
					await LoadTask(sp);
					break;
				case "Work":
					sp = "sp_nova_Dictionary_Work";
					await LoadWork(sp);
					break;

			}
			if (novas.Any()) selectedId = novas[0].NovaId;
		}
	}
}
