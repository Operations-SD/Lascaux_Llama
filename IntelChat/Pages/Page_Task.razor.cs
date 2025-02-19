using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Task = IntelChat.Models.Task;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Task
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public int? taskId { get; set; } // Task ID for navigation

		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; } = "list";
		private List<Pype> pypes = new List<Pype>();
		private List<Task> entities = new List<Task>();
		private Dictionary<String, Task> entity = new Dictionary<String, Task>();
		private Dictionary<String, String> filter = new Dictionary<String, String>();
		private string pageName = "Task";

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
				new SqlParameter("@task_label32", entity["add"].TaskLabel32),
				new SqlParameter("@task_type", entity["add"].TaskType),
				new SqlParameter("@task_status", entity["add"].TaskStatus),
				new SqlParameter("@task_level", entity["add"].TaskLevel),
				new SqlParameter("@task_description", entity["add"].TaskDescription),
				new SqlParameter("@task_duration", entity["add"].TaskDuration),
				new SqlParameter("@task_start_date", entity["add"].TaskStartDate),
				new SqlParameter("@task_finish_date", entity["add"].TaskFinishDate),
				new SqlParameter("@task_entry_date", entity["add"].TaskEntryDate),
				new SqlParameter("@task_previous", entity["add"].TaskPrevious),
				new SqlParameter("@person_id_fk", entity["add"].PersonIdFk),
				new SqlParameter("@nova_id_fk", entity["add"].NovaIdFk),
				new SqlParameter("@noun_id_fk", entity["add"].NounIdFk),
				new SqlParameter("@task_seq", entity["add"].TaskSeq),
				new SqlParameter("@task_parent", entity["add"].TaskParent),
				new SqlParameter("@task_url", entity["add"].TaskUrl)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Task]", parameters);
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
				new SqlParameter("@task_status", status),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure("dbo.[CRUD_Task]", parameters, true);
		}

		/// <summary>Load entities from the database into a list </summary>
		/// <param name="status">Status of the entities that will be loaded</param>
		/*
		private void LoadReadResults(string status = "*")
		{
			var reader = Read(status);
			if (reader == null) return;

			entities.Clear();
			while (reader.Read())
			{
				entities.Add(new Task
				{
					TaskId = reader.GetInt32(0),
					TaskLabel32 = reader.GetString(1),
					TaskType = reader.GetString(2),
					TaskStatus = reader.GetString(3),
					TaskLevel = reader.GetInt16(4),
					TaskDescription = reader.GetString(5),
					TaskDuration = reader.GetInt16(6),
					TaskStartDate = reader.GetDateTime(7),
					TaskFinishDate = reader.GetDateTime(8),
					TaskEntryDate = reader.GetDateTime(9),
					TaskPrevious = reader.GetInt32(10),
					PersonIdFk = reader.GetInt32(11),
					NovaIdFk = reader.GetInt32(12),
					NounIdFk = reader.GetInt32(13),
					PodIdFk = reader.GetInt32(14),
					TaskSeq = reader.GetInt16(15),
					TaskParent = reader.GetInt32(16),
					TaskUrl = reader.GetInt32(16)
				});
			}
			reader.Close();
		}
		*/

		
		private void LoadReadResults(string status = "*", string filter = "****")
		{
			var reader = Read(status, filter);
			if (reader == null) return;

			entities.Clear();
			while (reader.Read())
			{
				entities.Add(new Task
				{
					TaskId = reader.GetInt32(0),
					TaskLabel32 = reader.GetString(1),
					TaskType = reader.GetString(2),
					TaskStatus = reader.GetString(3),
					TaskLevel = reader.GetInt16(4),
					TaskDescription = reader.GetString(5),
					TaskDuration = reader.GetInt16(6),
					TaskStartDate = reader.GetDateTime(7),
					TaskFinishDate = reader.GetDateTime(8),
					TaskEntryDate = reader.GetDateTime(9),
					TaskPrevious = reader.GetInt32(10),
					PersonIdFk = reader.GetInt32(11),
					NovaIdFk = reader.GetInt32(12),
					NounIdFk = reader.GetInt32(13),
					PodIdFk = reader.GetInt32(14),
					TaskSeq = reader.GetInt16(15),
					TaskParent = reader.GetInt32(16),
					TaskUrl = reader.GetInt32(16)
				});
			}
			reader.Close();
		}
		

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.TaskType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.TaskStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@id", entity["change"].TaskId),
				new SqlParameter("@pod", pod),
				new SqlParameter("@task_label32", entity["change"].TaskLabel32),
				new SqlParameter("@task_type", entity["change"].TaskType),
				new SqlParameter("@task_status", entity["change"].TaskStatus),
				new SqlParameter("@task_level", entity["change"].TaskLevel),
				new SqlParameter("@task_description", entity["change"].TaskDescription),
				new SqlParameter("@task_duration", entity["change"].TaskDuration),
				new SqlParameter("@task_start_date", entity["change"].TaskStartDate),
				new SqlParameter("@task_finish_date", entity["change"].TaskFinishDate),
				new SqlParameter("@task_entry_date", entity["change"].TaskEntryDate),
				new SqlParameter("@task_previous", entity["change"].TaskPrevious),
				new SqlParameter("@person_id_fk", entity["change"].PersonIdFk),
				new SqlParameter("@nova_id_fk", entity["change"].NovaIdFk),
				new SqlParameter("@noun_id_fk", entity["change"].NounIdFk),
				new SqlParameter("@pod_id_fk", entity["change"].PodIdFk),
				new SqlParameter("@task_seq", entity["change"].TaskSeq),
				new SqlParameter("@task_parent", entity["change"].TaskParent),
				new SqlParameter("@task_url", entity["change"].TaskUrl)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Task]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].TaskId),
				new SqlParameter("@task_status", entity["delete"].TaskStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Task]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			entities.Add(entity["add"]);
			NotificationService.Notify("Task created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			entities.Remove(entities.Find(e => e.TaskId == entity["change"].TaskId));
			entities.Add(entity["change"]);
			NotificationService.Notify("Task changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			entities.Remove(entities.Find(e => e.TaskId == entity["delete"].TaskId));
			NotificationService.Notify("Task deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.TaskId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "Task"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Task", "Task"), true);
		}

		protected override void OnInitialized()
		{

			entity["add"] = new Task();
			entity["change"] = new Task();
			entity["delete"] = new Task();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			// Handle screen change options
			if (!string.IsNullOrEmpty(show))
			{
				switch (show)
				{
					case "change":
						if (taskId.HasValue)
						{
							AutoFill(taskId.Value, "change"); // Populate fields for specific NounId
						}
						else if (entities.Any())
						{
							entity["change"] = entities.First();
							AutoFill(entity["change"].TaskId, "change"); // Default to first entity
						}
						break;

					case "delete":
						var deletedEntity = entities.FirstOrDefault(e => e.TaskStatus == "D");
						if (deletedEntity != null)
						{
							entity["delete"] = deletedEntity;
							AutoFill(deletedEntity.TaskId, "delete"); // Populate fields for the deleted entity
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
					AutoFill(entity["change"].TaskId, "change");
				}
				show = string.Empty; // Do not show the list by default
			}


			/*
			entity["add"].TaskStartDate = DateTime.Today;
			entity["add"].TaskFinishDate = DateTime.Today;
			entity["add"].TaskEntryDate = DateTime.Today;

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].TaskId, "change");
			}

			if (entities.Find(e => e.TaskStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.TaskStatus == "D").First();
				AutoFill(entity["delete"].TaskId, "delete");
			}
			show = "list";
			*/
		}

		public void NavigateToLascaux()
		{
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Task", "Task"), true);
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
				var selectedEntity = entities.FirstOrDefault(entity => entity.TaskId == id);
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
				new SqlParameter("@task_status", status),
				new SqlParameter("@pod", pod)
			};

			return ExecuteStoredProcedure("dbo.[CRUD_Task]", parameters, true);
		}

		private void ApplyTagFilter(ChangeEventArgs e)
		{
			tagFilter = e.Value?.ToString() ?? string.Empty;
			LoadReadResults("*", tagFilter);
			//LoadReadResults("*");
		}


	}
}