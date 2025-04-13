using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using ThreadingTask = System.Threading.Tasks.Task;
using static IntelChat.Pages.Page_Nova;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Web;

namespace IntelChat.Pages
{
	/// *********************************************************************************
	/// *****************  Input ???? Parameters from Source Page Nav_to ****************
	/// *********************************************************************************
	public partial class Page_Lascaux
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; }

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
		public Lascaux novaLasc = new Lascaux();

		/// ****************************************************************************************
		/// *****************  Input LASCAUX - NOVA LIST  (not NULLS?) ******* MODEL ***************
		/// ****************************************************************************************
		public class Lascaux
		{
			public int NovaId { get; set; }
			public string? NovaDescription { get; set; }
			public string? NovaSubjectLabel { get; set; }
			public string? NovaActionLabel { get; set; }
			public string? NovaObjectLabel { get; set; }
			public string? NovaSubjectDescription { get; set; }
			public string? NovaActionDescription { get; set; }
			public string? NovaObjectDescription { get; set; }
			public string? SubjectURL { get; set; }
			public string? ActionURL { get; set; }
			public string? ObjectURL { get; set; }
			public int SubId { get; set; } // Subject NounId
			public int ActId { get; set; } // Action VerbId
			public int ObjId { get; set; } // Object NounId
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

		/*
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
		
		*/

		/// *********************************************************************************
		/// *********************** filter "****" get all records *************************** ????????
		/// *********************************************************************************
		private async Task<SqlDataReader?> Read(string sp, string filter = "****")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_Input_Filter", filter),
				new SqlParameter("@noun", subid),
				new SqlParameter("@verb", actid),
				new SqlParameter("@object", objid),
				new SqlParameter("@nova",Page_Task.novaT.novaParamValue)
			};
			return await ExecuteStoredProcedure($"dbo.[{sp}]", parameters, true);
		}

		/*
		private SqlDataReader? CreateTempTable(string sp, string status = "*")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@noun", tempVariable.nounId)
				//new SqlParameter("@noun", subid)
			};
			return ExecuteStoredProcedure2($"dbo.[{sp}]", parameters, true);
		}

		*/

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

		/*
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
		
		*/

		private async ThreadingTask LoadSubject(string sp, string filter = "SUBJECT")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}
		
		private async ThreadingTask LoadAction(string sp, string filter = "ACTION")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}
		
		private async ThreadingTask LoadNova(string sp, string filter = "NOVA")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}
		
		private async ThreadingTask LoadObject(string sp, string filter = "OBJECT")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}


		private async ThreadingTask LoadLascaux(string sp, string filter = "****")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}
		
		private async ThreadingTask LoadUrl(string sp, string filter = "URL")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}


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
		
		
		private async ThreadingTask LoadGuide(string sp, string filter = "GUIDE")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}



		/// ************************ LOAD Question **********************************************
		/// ************************ LOAD Question **********************************************
		/// ************************ LOAD Question **********************************************
		private async ThreadingTask LoadQuestion(string sp, string filter = "QUESTION")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}
		
		private async ThreadingTask LoadLocation(string sp, string filter = "LOCATION")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
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
		private async ThreadingTask LoadTask(string sp, string filter = "TASK")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
				});
			}

			reader.Close();
		}


		/// ************************ LOAD Work *****************************************
		/// ************************ LOAD Work *****************************************
		/// ************************ LOAD Work *****************************************
		private async ThreadingTask LoadWork(string sp, string filter = "WORK")
		{
			var reader = await Read(sp, filter);
			if (reader == null) return;

			List<LascauxFromNova> entities = new List<LascauxFromNova>();
			while (await reader.ReadAsync())
			{
				entities.Add(new LascauxFromNova
				{
					NovaIn = !reader.IsDBNull(0) ? reader.GetString(0) : "",
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
			/// **********************************************************************   ??????????????????????????????????????????
			/// entities = entities.FindAll(entity => (subid == 0 || entity.S == subid)
			///					&& (actid == 0 || entity.A == actid)
			///					&& (objid == 0 || entity.O == objid)
			///					&& pid == entity.Pid);
			novas.Clear();
			foreach (LascauxFromNova entity in entities)
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
					ObjectURL = entity.ObjectUrl ?? "",
					SubId = entity.S, // Populate Subject NounId
					ActId = entity.A,
					ObjId = entity.O
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
				case "SUBJECT":
					sp = "sp_Lascaux_#temp";
					await LoadSubject(sp);
					//Console.WriteLine($"Test variable dynamically updated to Lascaux: {subid}");
					break;
				
				case "NOVA":
					sp = "sp_Lascaux_#temp";
					await LoadNova(sp);
					//Console.WriteLine($"Test variable dynamically updated to Lascaux: {subid}");
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
				case "TASK":
					sp = "sp_Lascaux_#temp";
					await LoadTask(sp);
					break;
				case "Work":
					sp = "sp_Lascaux_#temp";
					await LoadWork(sp);
					break;
				case "GUIDE":
					sp = "sp_Lascaux_#temp";
					await LoadGuide(sp);
					break;
				case "QUESTION":
					sp = "sp_Lascaux_#temp";
					await LoadQuestion(sp);
					break;
				case "URL":
					sp = "sp_Lascaux_#temp";
					await LoadUrl(sp);
					break;
				case "LOCATION":
					sp = "sp_Lascaux_#temp";
					await LoadLocation(sp);
					break;
				case "ACTION":
					sp = "sp_Lascaux_#temp";
					await LoadAction(sp);
					break;
				case "OBJECT":
					sp = "sp_Lascaux_#temp";
					await LoadObject(sp);
					break;
				default:
					sp = "sp_Lascaux_#temp";
					await LoadLascaux(sp);
					break;

			}
			if (novas.Any()) selectedId = novas[0].NovaId;
		}
		private bool subjectIsDragging = false, actionIsDragging = false, objectIsDragging = false;
		private bool subjectIsInsideDropZone = false, actionIsInsideDropZone = false, objectIsInsideDropZone = false;

		private (int X, int Y) subjectImagePosition = (324, 522);
		private (int X, int Y) actionImagePosition = (575, 522);
		private (int X, int Y) objectImagePosition = (826, 522);

		private (int X, int Y) mouseStart = (0, 0);
		private readonly (int Width, int Height) originalImageSize = (200, 200);
		private (int Width, int Height) resizedImageSize = (98, 98); // Resized image dimensions for drop zone

		private string subjectZIndex = "1000";
		private string actionZIndex = "1000";
		private string objectZIndex = "1000";

		private string dropMessage = ""; // Message for invalid drops

		// Drop zone dimensions and positions with calculated left coordinates
		private readonly (int X, int Y, int Width, int Height) subjectObjectDropZone = (1253, -2, 98, 98); // Adjusted for "right" position
		private readonly (int X, int Y, int Width, int Height) actionDropZone = (1253, 111, 98, 98);      // Adjusted for "right" position


		// Start dragging logic
		private void StartDragging(string type, MouseEventArgs e)
		{
			mouseStart = type switch
			{
				"Subject" => ((int)e.ClientX - subjectImagePosition.X, (int)e.ClientY - subjectImagePosition.Y),
				"Action" => ((int)e.ClientX - actionImagePosition.X, (int)e.ClientY - actionImagePosition.Y),
				"Object" => ((int)e.ClientX - objectImagePosition.X, (int)e.ClientY - objectImagePosition.Y),
				_ => mouseStart
			};

			if (type == "Subject") subjectIsDragging = true;
			if (type == "Action") actionIsDragging = true;
			if (type == "Object") objectIsDragging = true;

			dropMessage = ""; // Clear any previous message
		}

		// Stop dragging logic
		private void StopDragging(string type, MouseEventArgs e)
		{
			if (type == "Subject") subjectIsDragging = false;
			if (type == "Action") actionIsDragging = false;
			if (type == "Object") objectIsDragging = false;

			HandleDrop(e.ClientX, e.ClientY, type);
		}

		// Mouse move logic
		private void OnMouseMove(string type, MouseEventArgs e)
		{
			if (type == "Subject" && subjectIsDragging)
			{
				subjectImagePosition = ((int)e.ClientX - mouseStart.X, (int)e.ClientY - mouseStart.Y);
			}
			else if (type == "Action" && actionIsDragging)
			{
				actionImagePosition = ((int)e.ClientX - mouseStart.X, (int)e.ClientY - mouseStart.Y);
			}
			else if (type == "Object" && objectIsDragging)
			{
				objectImagePosition = ((int)e.ClientX - mouseStart.X, (int)e.ClientY - mouseStart.Y);
			}
		}

		// Handle drop logic
		private void HandleDrop(double mouseX, double mouseY, string type)
		{
			bool isInsideSubjectObjectDropZone =
				mouseX >= subjectObjectDropZone.X &&
				mouseX <= (subjectObjectDropZone.X + subjectObjectDropZone.Width) &&
				mouseY >= subjectObjectDropZone.Y &&
				mouseY <= (subjectObjectDropZone.Y + subjectObjectDropZone.Height);

			bool isInsideActionDropZone =
				mouseX >= actionDropZone.X &&
				mouseX <= (actionDropZone.X + actionDropZone.Width) &&
				mouseY >= actionDropZone.Y &&
				mouseY <= (actionDropZone.Y + actionDropZone.Height);

			// Handle Subject and Object drop
			if ((type == "Subject" || type == "Object") && isInsideSubjectObjectDropZone)
			{
				SnapToDropZone(subjectObjectDropZone, type, "1006"); // Align image in drop zone

				// Pass the corresponding NounId for Subject or Object
				int? nounId = GetId(type);
				if (nounId.HasValue)
				{
					NavigateToPage(type, nounId.Value);
				}
			}

			// Handle Action drop
			else if (type == "Action" && isInsideActionDropZone)
			{
				SnapToDropZone(actionDropZone, type, "1006"); // Higher z-index after dropping

				int? verbId = GetId(type);
				if (verbId.HasValue)
				{
					NavigateToPage(type, verbId.Value);
				}
			}

			// Invalid drop
			else
			{
				dropMessage = $"{type} does not belong in this drop zone.";
			}
		}

		// Snap the image to the drop zone
		private void SnapToDropZone((int X, int Y, int Width, int Height) dropZone, string type, string newZIndex)
		{
			var position = (
				dropZone.X + (dropZone.Width - resizedImageSize.Width) / 2,
				dropZone.Y + (dropZone.Height - resizedImageSize.Height) / 2
			);

			switch (type)
			{
				case "Subject":
					subjectImagePosition = position;
					subjectIsInsideDropZone = true;
					subjectZIndex = newZIndex; // Update z-index
					break;
				case "Action":
					actionImagePosition = position;
					actionIsInsideDropZone = true;
					actionZIndex = newZIndex; // Update z-index
					break;
				case "Object":
					objectImagePosition = position;
					objectIsInsideDropZone = true;
					objectZIndex = newZIndex; // Update z-index
					break;
			}
		}

		private int? GetId(string type)
		{
			int? id = type switch
			{
				"Subject" => novaLasc?.SubId,
				"Action" => novaLasc?.ActId,
				"Object" => novaLasc?.ObjId,
				_ => null
			};
			return id;
		}

		private void NavigateToPage(string type, int typeId)
		{
			string url = type switch
			{
				"Subject" => $"/Noun?pod={pod}&pid={pid}&prevPage=Lascaux&show=change&type=NounSubject&nounId={typeId}",
				"Object" => $"/Noun?pod={pod}&pid={pid}&prevPage=Lascaux&show=change&type=NounObject&nounId={typeId}",
				"Action" => $"/Verb?pod={pod}&pid={pid}&prevPage=Lascaux&show=change&type=NounVerb&verbId={typeId}",
				_ => throw new InvalidOperationException($"Unknown type: {type}")
			};

			NavigationManager.NavigateTo(url);
		}
	}
}