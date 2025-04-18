using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_NovaCrud
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public int? novaId { get; set; }

		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; }
		private List<Pype> pypes = new List<Pype>();
		private List<Nova> entities = new List<Nova>();
		private Dictionary<String, Nova> entity = new Dictionary<String, Nova>();
		private Dictionary<String, String> filter = new Dictionary<String, String>();
		private string pageName = "NOVA";

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
				new SqlParameter("@pod", pod),
				new SqlParameter("@nova_description", entity["add"].NovaDescription),
				new SqlParameter("@nova_type", entity["add"].NovaType),
				new SqlParameter("@nova_channel", entity["add"].NovaChannel),
				new SqlParameter("@nova_subject", entity["add"].NovaSubject),
				new SqlParameter("@nova_action", entity["add"].NovaAction),
				new SqlParameter("@nova_object", entity["add"].NovaObject),
				new SqlParameter("@nova_datetime", entity["add"].NovaDatetime),
				new SqlParameter("@pod", entity["add"].PodIdFk),
				new SqlParameter("@person_id_fk", entity["add"].PersonIdFk),
				new SqlParameter("@nova_priority", entity["add"].NovaPrioriy),
				new SqlParameter("@nova_label", entity["add"].NovaLabel),
				new SqlParameter("@nova_tag", entity["add"].NovaTag)
			};
			ExecuteStoredProcedure("dbo.[CRUD_NOVA]", parameters);
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
				new SqlParameter("@nova_status", status),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure("dbo.[CRUD_NOVA]", parameters, true);
		}

		/// <summary>Load entities from the database into a list </summary>
		/// <param name="status">Status of the entities that will be loaded</param>
		private void LoadReadResults(string status = "*", string filter = "****")
		{
			var reader = Read(status, filter);
			if (reader == null) return;

			entities.Clear();
			while (reader.Read())
			{
				entities.Add(new Nova
				{
					NovaId = reader.GetInt32(0),
					NovaDescription = reader.GetString(1),
					NovaType = reader.GetString(2),
					NovaStatus = reader.GetString(3),
					NovaChannel = reader.GetInt32(4),
					NovaSubject = reader.GetInt32(5),
					NovaAction = reader.GetInt32(6),
					NovaObject = reader.GetInt32(7),
					NovaDatetime = reader.GetDateTime(8),
					PodIdFk = reader.GetInt32(9),
					PersonIdFk = reader.GetInt32(10),
					NovaPrioriy = reader.GetInt16(11),
					NovaLabel = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),
					NovaTag = reader.IsDBNull(13) ? string.Empty : reader.GetString(13)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.NovaType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.NovaStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
					new SqlParameter("@PROC_action", "Update"),
					new SqlParameter("@nova_id", entity["change"].NovaId),
					new SqlParameter("@pod", pod),
					new SqlParameter("@nova_description", entity["change"].NovaDescription),
					new SqlParameter("@nova_status", entity["change"].NovaStatus),
					new SqlParameter("@nova_type", entity["change"].NovaType),
					new SqlParameter("@nova_channel", entity["change"].NovaChannel),
					new SqlParameter("@nova_subject", entity["change"].NovaSubject),
					new SqlParameter("@nova_action", entity["change"].NovaAction),
					new SqlParameter("@nova_object", entity["change"].NovaObject),
					new SqlParameter("@nova_datetime", entity["change"].NovaDatetime),
					new SqlParameter("@person_id_fk", entity["change"].PersonIdFk),
					new SqlParameter("@nova_priority", entity["change"].NovaPrioriy),
					new SqlParameter("@nova_label", entity["change"].NovaLabel),
					new SqlParameter("@nova_tag", entity["change"].NovaTag)
			};
			ExecuteStoredProcedure("dbo.[CRUD_NOVA]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@nova_id", entity["delete"].NovaId),
				new SqlParameter("@nova_status", entity["delete"].NovaStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_NOVA]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("NOVA created successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("NOVA changed successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("NOVA deleted successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.NovaId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "NOVA"),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure("dbo.[sp_Pype_Type_Locked]", parameters, true);
		}

		/// <summary>Load pypes from the database into a list</summary>
		private void LoadReadPypeResults()
		{
			var reader = ReadPype();
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
					PypeLink = reader.GetString(5)
				});
			}
			reader.Close();
		}

		public void NavigateToLascaux(bool isSubject)
		{
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "NovaCrud", "NovaCrud"), true);
		}

		protected override void OnInitialized()
		{
			// Initialize default entities and filters
			entity["add"] = new Nova();
			entity["change"] = new Nova();
			entity["delete"] = new Nova();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			// Load data for Nova entities
			LoadReadResults();
			LoadReadPypeResults();

			// Handle screen change options
			if (!string.IsNullOrEmpty(show))
			{
				switch (show)
				{
					case "change":
						if (novaId.HasValue)
						{
							AutoFill(novaId.Value, "change"); // Populate fields for specific NovaId
						}
						else if (entities.Any())
						{
							entity["change"] = entities.First();
							AutoFill(entity["change"].NovaId, "change"); // Default to first entity
						}
						break;

					case "delete":
						var deletedEntity = entities.FirstOrDefault(e => e.NovaStatus == "D");
						if (deletedEntity != null)
						{
							entity["delete"] = deletedEntity;
							AutoFill(deletedEntity.NovaId, "delete"); // Populate fields for the deleted entity
						}
						break;

					case "list":
						show = "list"; // Explicitly requested, so show the list
						break;

					default:
						show = string.Empty; // Prevent default listing when navigating normally
						break;
				}
			}
			else
			{
				// Default behavior if `show` is not specified
				if (entities.Any())
				{
					entity["change"] = entities.First();
					AutoFill(entity["change"].NovaId, "change");
				}
				show = string.Empty; // Do not show the list by default
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
				var selectedEntity = entities.FirstOrDefault(entity => entity.NovaId == id);
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
				new SqlParameter("@nova_status", status),
				new SqlParameter("@pod", pod)
			};

			return ExecuteStoredProcedure("dbo.[CRUD_Nova]", parameters, true);
		}

		private void ApplyTagFilter(ChangeEventArgs e)
		{
			tagFilter = e.Value?.ToString() ?? string.Empty;
			LoadReadResults("*", tagFilter);
		}
	}
}