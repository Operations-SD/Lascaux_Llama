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
	public partial class Page_Question
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; } = "list";
		private List<Pype> pypes = new List<Pype>();
		private List<Question> entities = new List<Question>();
		private Dictionary<String, Question> entity = new Dictionary<String, Question>();
		private Dictionary<String, String> filter = new Dictionary<String, String>();

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
				new SqlParameter("@Question_ID", entity["add"].QuestionId),
				new SqlParameter("@Question_Text", entity["add"].QuestionText),
				new SqlParameter("@Question_Type", entity["add"].QuestionType),
				new SqlParameter("@Question_Status", entity["add"].QuestionStatus),
				new SqlParameter("@NOVA_ID_FK", entity["add"].NovaIdFk)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Question]", parameters);
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
				new SqlParameter("@Question_status", status)
			};
			return ExecuteStoredProcedure("dbo.[CRUD_Question]", parameters, true);
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
				entities.Add(new Question
				{
					QuestionId = reader.GetInt32(0),
					QuestionText = reader.GetString(1),
					QuestionType = reader.GetString(2),
					QuestionStatus = reader.GetString(3),
					NovaIdFk = reader.GetInt32(5)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.QuestionType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.QuestionStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@Question_ID", entity["change"].QuestionId),
				new SqlParameter("@Question_Text", entity["change"].QuestionText),
				new SqlParameter("@Question_Type", entity["change"].QuestionType),
				new SqlParameter("@Question_Status", entity["change"].QuestionStatus),
				new SqlParameter("@NOVA_ID_FK", entity["change"].NovaIdFk)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Question]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].QuestionId),
				new SqlParameter("@Question_Status", entity["delete"].QuestionStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Question]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("Question created successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("Question changed successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("Question deleted successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.QuestionId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "QSTN"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Question", "Question"), true);
		}

		protected override void OnInitialized()
		{
			entity["add"] = new Question();
			entity["change"] = new Question();
			entity["delete"] = new Question();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].QuestionId, "change");
			}

			if (entities.Find(e => e.QuestionStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.QuestionStatus == "D").First();
				AutoFill(entity["delete"].QuestionId, "delete");
			}
			show = "list";
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

        private async ThreadingTask HandleDirectSelectKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && int.TryParse(directSelectId, out int id))
            {
                // Find the entity by ID and navigate to the change screen
                var selectedEntity = entities.FirstOrDefault(entity => entity.QuestionId == id);
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
                new SqlParameter("@question_status", status)
            };

            return ExecuteStoredProcedure("dbo.[CRUD_Question]", parameters, true);
        }

        private void ApplyTagFilter(ChangeEventArgs e)
        {
            tagFilter = e.Value?.ToString() ?? string.Empty;
            LoadReadResults("*", tagFilter);
        }
    }
}