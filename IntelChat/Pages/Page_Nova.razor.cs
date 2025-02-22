using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Nova
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		public int id = 0;
		public string description = "Intel-a-Chat NOVA";
		                             //*****************************************************************************************
		public string type = "JUNK"; // *************** Lascaux Case Switch - NOVA or POD,TASK,WORK,NOUN,VERB,QUESTION,INTERVIEW//*****************************************************************************************
		
		/*
		public static class tempVariable
		{
			public static int nounId; // Temporary variable to hold the current NounId
		}
		
		public int _subject;
		public int subject
		{
			get => _subject;
			set
			{
				if (_subject != value) // Check to avoid unnecessary updates
				{
					_subject = value;
					tempVariable.nounId = _subject; // Dynamically update the test variable
					//Console.WriteLine($"Test variable dynamically updated to: {tempVariable.nounId}");
				}
			}
		}
		*/

		public int subject = 0;
		public int action = 0;
		public int obj = 0;
		public string? StoredNoun { get; set; }
		public string? StoredVerb { get; set; }
		public string status = "";
		private List<Pype> pypes = new List<Pype>();
		private List<Noun> nouns = new List<Noun>();
		private List<Verb> verbs = new List<Verb>();
		private List<Noun> subjects = new List<Noun>();
		private List<Verb> actions  = new List<Verb>();
		private List<Noun> objects  = new List<Noun>();
		[Inject]
		public NotificationService NotificationService { get; set; }


		/**********************************************************************************/
		/************************ PYPE dropdown filters ***********************************/
		/**********************************************************************************/
		private string _subjectFilter = "";
		public string subjectFilter
		{
			get { return _subjectFilter; }
			set
			{
				if (_subjectFilter != value)
				{
					_subjectFilter = value;
					if (subjectFilter == "****") subjects = nouns;
					else subjects = nouns.FindAll(noun => noun.NounType == subjectFilter);
				}
			}
		}

		private string _actionFilter = "";
		public string actionFilter
		{
			get { return _actionFilter; }
			set
			{
				if (_actionFilter != value)
				{
					_actionFilter = value;
					if (actionFilter == "****") actions = verbs;
					else actions = verbs.FindAll(verb => verb.VerbType == actionFilter);
				}
			}
		}

		private string _objectFilter = "";
		public string objectFilter
		{
			get { return _objectFilter; }
			set
			{
				if (_objectFilter != value)
				{
					_objectFilter = value;
					if (objectFilter == "****") objects = nouns;
					else objects = nouns.FindAll(noun => noun.NounType == objectFilter);
				}
			}
		}

		public void OnCreate()
		{
			if (subject > 0 && action > 0 && obj > 0)
			{
				CreateNOVA("CRUD_NOVA");
			}
			else NotificationService.Notify("ERROR: Please select a Subject, Action, and Object", NotificationType.Error);
		}


		/*************************************************************************************/
		/************************ Stored Procedure - Server Connection  **********************/
		/*************************************************************************************/
		private SqlDataReader? ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQueryAsync();
			return null;
		}
		
		private (string Noun, string Verb)? ReadNounVerb()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod ?? 0) // Ensure a default value if `pod` is null
			};

			using var reader = ExecuteStoredProcedure("dbo.sp_Pype_NOVA_Test", parameters, true);
			if (reader != null && reader.Read())
			{
				string noun = reader["Noun"]?.ToString() ?? string.Empty;
				string verb = reader["Verb"]?.ToString() ?? string.Empty;
				reader.Close();
				return (noun, verb);
			}
			return null;
		}
		

		/**********************************************************************************/
		/************************ Stored Procedure - LOAD PYPE values *********************/
		/**********************************************************************************/
		private SqlDataReader? ReadPypes(string filter)
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", filter),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure("dbo.[sp_Pype_Type_Locked]", parameters, true);
		}

		private void LoadPypes()
		{
			var result = ReadNounVerb();
			StoredNoun = result.Value.Noun;
			StoredVerb = result.Value.Verb;
			
			var reader = ReadPypes(StoredNoun);
			if (reader == null) return;
			
			pypes.Clear();
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
					PodIdFk = reader.GetInt32(6)
				});
			}
			reader.Close();

			reader = ReadPypes(StoredVerb);
			if (reader == null) return;
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
					PodIdFk = reader.GetInt32(6)
				});
			}
			reader.Close();
		}


		/**********************************************************************************/
		/************************ Stored Procedure - POD NOUN values **********************/
		/**********************************************************************************/
		private SqlDataReader? ReadNouns(string sp, string status = "*")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_filter", "****"),
				new SqlParameter("@status", status),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure($"dbo.[{sp}]", parameters, true);
		}

		private void LoadNouns(string sp, string status = "*")
		{
			var reader = ReadNouns(sp, status);
			if (reader == null) return;

			nouns.Clear();
			while (reader.Read())
			{
				nouns.Add(new Noun
				{
					NounId = reader.GetInt32(0),
					NounLabel = reader.GetString(1),
					NounDescription = reader.GetString(2),
					NounType = reader.GetString(3),
					NounStatus = reader.GetString(4),
					PodIdFk = reader.GetInt32(5),
					UrlIdPk = reader.GetInt32(6)
				});
			}
			reader.Close();
		}


		/**********************************************************************************/
		/************************ Stored Procedure - POD VERB values **********************/
		/**********************************************************************************/
		private SqlDataReader? ReadVerb(string sp, string status = "*")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_filter", "****"),
				new SqlParameter("@status", status),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure($"dbo.[{sp}]", parameters, true);
		}

		private void LoadVerbs(string sp, string status = "*")
		{
			var reader = ReadVerb(sp, status);
			if (reader == null) return;

			verbs.Clear();
			while (reader.Read())
			{
				verbs.Add(new Verb
				{
					VerbId = reader.GetInt32(0),
					VerbLabel = reader.GetString(1),
					VerbDescription = reader.GetString(2),
					VerbType = reader.GetString(3),
					VerbStatus = reader.GetString(4),
					PodIdFk = reader.GetInt32(5),
					UrlIdPk = reader.GetInt32(6)
				});
			}
			reader.Close();
		}


		/**********************************************************************************/
		/************************ Stored Procedure - CREATE a NOVA ************************/
		/**********************************************************************************/
		private void CreateNOVA(string sp)
		{
			string spName = $"dbo.[{sp}]";

			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand(spName, connection);

			SqlParameter param1 = new SqlParameter("@PROC_action", "Create");
			SqlParameter param2 = new SqlParameter("@nova_subject", subject);
			SqlParameter param3 = new SqlParameter("@nova_action", action);
			SqlParameter param4 = new SqlParameter("@nova_object", obj);
			SqlParameter param5 = new SqlParameter("@nova_description", description);
			SqlParameter param6 = new SqlParameter("@nova_type", type);
			SqlParameter param7 = new SqlParameter("@nova_datetime", DateTime.Now);
			SqlParameter param8 = new SqlParameter("@pod", pod);
			SqlParameter param9 = new SqlParameter("@person_id_fk", pid);

			cmd.Parameters.Add(param1);
			cmd.Parameters.Add(param2);
			cmd.Parameters.Add(param3);
			cmd.Parameters.Add(param4);
			cmd.Parameters.Add(param5);
			cmd.Parameters.Add(param6);
			cmd.Parameters.Add(param7);
			cmd.Parameters.Add(param8);
			cmd.Parameters.Add(param9);

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			int result = cmd.ExecuteNonQuery();
			if (result == 0)
				NotificationService.Notify("NOVA created unsuccessfully!", NotificationType.Error); 
			else
				NotificationService.Notify("NOVA created successfully!", NotificationType.Success);
		}


		/**********************************************************************************/
		/************************ Cloud Server - Stored Procedure *************************/
		/**********************************************************************************/
		protected override void OnInitialized()
		{
			LoadPypes();
			LoadNouns("CRUD_Noun", "A");
			LoadVerbs("CRUD_Verb", "A");
			subjects = new List<Noun>(nouns);
			actions  = new List<Verb>(verbs);
			objects  = new List<Noun>(nouns);
		}
	}
}