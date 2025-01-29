using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Data;
using System.Data.SqlClient;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Verb
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? verbId { get; set; } // Noun ID for navigation
		[Parameter]
		[SupplyParameterFromQuery]
		public string? show { get; set; } // Tab to display
		[Parameter]
		[SupplyParameterFromQuery]
		public string? type { get; set; } // Type of noun (Subject or Object)

		public string? StoredVerb { get; set; }
		
		[Inject]
        public NotificationService NotificationService { get; set; }
		private List<Pype> pypes = new List<Pype>();
		private List<Verb> entities = new List<Verb>();
		private Dictionary<String, Verb> entity = new Dictionary<String, Verb>();
		private Dictionary<String, String> filter = new Dictionary<String, String>();
		private string pageName = "Verb";

		/// <summary>Executes a stored procedure and (optionally) returns a reader for its results</summary>
		/// <param name="procedure">Name of the stored procedure</param>
		/// <param name="parameters">List of arguments for the stored procedure</param>
		/// <param name="reader">Whether a reader for the stored procedure results should be returned</param>
		/// <returns>
		/// Reader for the results of the stored procedure, if reader = true
		/// null, if reader = false
		/// </returns>
		private SqlDataReader? ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQuery();
			return null;
		}

		/// <summary>Creates a new entity in the database using a stored procedure</summary>
		private void Create()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Create"),
				new SqlParameter("@label", entity["add"].VerbLabel),
				new SqlParameter("@desc", entity["add"].VerbDescription),
				new SqlParameter("@type", entity["add"].VerbType),
				new SqlParameter("@status", entity["add"].VerbStatus),
				new SqlParameter("@pod", pod)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Verb]", parameters);
		}


		/// <summary>Reads entities from the database using a stored procedure</summary>
		/// <param name="status">Status of the entities that will be read</param>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? Read(string status = "*")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_filter", "****"),
				new SqlParameter("@status", status),
				new SqlParameter("@pod", pod)
			};

			return ExecuteStoredProcedure("dbo.[CRUD_Verb]", parameters, true);
		}

		/// <summary>Load entities from the database into a list </summary>
		/// <param name="status">Status of the entities that will be loaded</param>
		private void LoadReadResults(string status = "*", string filter = "****")
		{
			var reader = Read(status);
			if (reader == null) return;

			entities.Clear();
			while (reader.Read())
			{
				entities.Add(new Verb
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

        /// <summary>Synchronizes entities from the database into a singleton service, after reading. </summary>
        /// <param name="status">Status of the entities that will be loaded</param>
        //private void SyncAndLoadReadResults(string status = "*")
        //{
        //    LoadReadResults(status);
        //   verbService.Verbs = this.entities;
        //}

        /// <summary>Handle events triggered by the change of the change filter select</summary>
        /// <param name="args">Arguments from a filter change event</param>
		/// 
        private void OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(verb => verb.VerbType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.VerbStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
					new SqlParameter("@PROC_action", "Update"),
					new SqlParameter("@id", entity["change"].VerbId),
					new SqlParameter("@label", entity["change"].VerbLabel),
					new SqlParameter("@desc", entity["change"].VerbDescription),
					new SqlParameter("@type", entity["change"].VerbType),
					new SqlParameter("@status", entity["change"].VerbStatus),
					new SqlParameter("@url_id_pk", entity["change"].UrlIdPk),
					new SqlParameter("@pod", pod)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Verb]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].VerbId),
				new SqlParameter("@status", entity["delete"].VerbStatus)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Verb]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			entities.Add(entity["add"]);
		//	SyncAndLoadReadResults();
			NotificationService.Notify("Verb created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			entities.Remove(entities.Find(e => e.VerbId == entity["change"].VerbId));
			entities.Add(entity["change"]);
		//	SyncAndLoadReadResults();
			NotificationService.Notify("Verb changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			entities.Remove(entities.Find(e => e.VerbId == entity["delete"].VerbId));
		//	SyncAndLoadReadResults();
			NotificationService.Notify("Verb deleted successfully!", NotificationType.Success);
			show = "list";
		}


		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.VerbId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		
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
		
		
		private SqlDataReader? ReadPype(string filter)
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", filter),
				new SqlParameter("@pod", pod)
			};

			return ExecuteStoredProcedure("dbo.[sp_Pype_Type_Locked]", parameters, true);
		}

		/// <summary>Load pypes from the database into a list</summary>
		private void LoadReadPypeResults()
		{
			var result = ReadNounVerb();
			StoredVerb = result.Value.Noun;
			
			var reader = ReadPype(StoredVerb);
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
				});
			}
			reader.Close();
		}

		//********************** Override the OnInitialized method ********************
		//********************** ReRead lists when necessary ********************

		protected override void OnInitialized()
		{
			// Initialize default entities and filters
			entity["add"] = new Verb();
			entity["change"] = new Verb();
			entity["delete"] = new Verb();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			// Load data for Noun entities
			LoadReadResults();
			LoadReadPypeResults();

			// Handle screen change options
			if (!string.IsNullOrEmpty(show))
			{
				switch (show)
				{
					case "change":
						if (verbId.HasValue)
						{
							AutoFill(verbId.Value, "change"); // Populate fields for specific NounId
						}
						else if (entities.Any())
						{
							entity["change"] = entities.First();
							AutoFill(entity["change"].VerbId, "change"); // Default to first entity
						}
						break;

					case "delete":
						var deletedEntity = entities.FirstOrDefault(e => e.VerbStatus == "D");
						if (deletedEntity != null)
						{
							entity["delete"] = deletedEntity;
							AutoFill(deletedEntity.VerbId, "delete"); // Populate fields for the deleted entity
						}
						break;

					case "list":
					default:
						show = "list"; // Default to list view
						break;
				}
			}
			else
			{
				// Default behavior if `show` is not specified
				Console.WriteLine("No screen change option provided, defaulting to 'list' tab.");
				if (entities.Any())
				{
					entity["change"] = entities.First();
					AutoFill(entity["change"].VerbId, "change");
				}
				show = "list";
			}
		}

		private void OnItemSelected(int id)
		{
			// Find the selected entity by ID and set it for the change form
			AutoFill(id, "change");
			show = "change"; // Navigate to the change screen
		}

		// <summary> Handle item selection from entering ID into field </summary>
		private string directSelectId = string.Empty;

		private void UpdateDirectSelectId(ChangeEventArgs e)
		{
			directSelectId = e.Value?.ToString() ?? string.Empty; // Update the input value
		}

		private async System.Threading.Tasks.Task HandleDirectSelectKeyPress(KeyboardEventArgs e)
		{
			if (e.Key == "Enter" && int.TryParse(directSelectId, out int id))
			{
				// Find the entity by ID and navigate to the change screen
				var selectedEntity = entities.FirstOrDefault(entity => entity.VerbId == id);
				if (selectedEntity != null)
				{
					AutoFill(id, "change"); // Populate fields with selected entity
					show = "change";        // Switch to the change screen
					await InvokeAsync(StateHasChanged); // Ensure immediate UI re-render
				}
				else
				{
					NotificationService.Notify("Invalid ID entered!", NotificationType.Error);
				}
			}
		}

		private string tagFilter { get; set; } = string.Empty;
		private SqlDataReader? Read(string status = "*", string filter = "****")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_filter", filter),
				new SqlParameter("@status", status),
				new SqlParameter("@pod", pod)
			};

			return ExecuteStoredProcedure("dbo.[CRUD_Verb]", parameters, true);
		}

		private void ApplyTagFilter(ChangeEventArgs e)
		{
			tagFilter = e.Value?.ToString() ?? string.Empty;
			LoadReadResults("*", tagFilter);
		}
	}
}