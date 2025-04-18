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
	public partial class Page_URL
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
        [Parameter]
        [SupplyParameterFromQuery]
        public int? urlId { get; set; } // Url ID for navigation

        [Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; } = "list";
		private List<Pype> pypes = new List<Pype>();
		private List<Url> entities = new List<Url>();
		private Dictionary<String, Url> entity = new Dictionary<String, Url>();
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
				new SqlParameter("@URL_label", entity["add"].UrlLabel),
				new SqlParameter("@URL_Description", entity["add"].UrlDescription),
				new SqlParameter("@URL_type", entity["add"].UrlType),
				new SqlParameter("@URL_status", entity["add"].UrlStatus),
				new SqlParameter("@URL_cloud", _configuration.GetValue<string>("ConnectionStrings:BlobUrlRoot") + entity["add"].UrlCloud),
				new SqlParameter("@URL_advance_level", entity["add"].UrlAdvanceLevel),
				new SqlParameter("@NOVA_ID_FK", entity["add"].NovaIdFk),
				new SqlParameter("@URL_tag", entity["add"].UrlTag),
				new SqlParameter("@URL_chain", entity["add"].UrlChain),
				new SqlParameter("@URL_stars", entity["add"].UrlStars)
			};
			ExecuteStoredProcedure("dbo.[CRUD_URL]", parameters);
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
				new SqlParameter("@URL_status", status)
				//pod?
			};
			return ExecuteStoredProcedure("dbo.[CRUD_URL]", parameters, true);
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
				entities.Add(new Url
				{
					UrlId = reader.GetInt32(0),
					UrlLabel = reader.GetString(1),
					UrlDescription = reader.GetString(2),
					UrlType = reader.GetString(3),
					UrlStatus = reader.GetString(4),
					UrlCloud = reader.GetString(5),
					UrlAdvanceLevel = reader.GetByte(6),
					NovaIdFk = reader.GetInt32(7),
					UrlTag = reader.GetString(8),
					UrlChain = reader.GetInt32(9),
					UrlStars = reader.GetByte(10)					
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.UrlType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.UrlStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@URL_id", entity["change"].UrlId),
				new SqlParameter("@URL_label", entity["change"].UrlLabel),
				new SqlParameter("@URL_Description", entity["change"].UrlDescription),
				new SqlParameter("@URL_type", entity["change"].UrlType),
				new SqlParameter("@URL_status", entity["change"].UrlStatus),
				new SqlParameter("@URL_cloud", entity["change"].UrlCloud),
				new SqlParameter("@URL_advance_level", entity["change"].UrlAdvanceLevel),
				new SqlParameter("@NOVA_ID_FK", entity["change"].NovaIdFk),
				new SqlParameter("@URL_tag", entity["change"].UrlTag),
				new SqlParameter("@URL_chain", entity["change"].UrlChain),
				new SqlParameter("@URL_stars", entity["change"].UrlStars)
			};
			ExecuteStoredProcedure("dbo.[CRUD_URL]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@URL_ID", entity["delete"].UrlId),
				new SqlParameter("@URL_status", entity["delete"].UrlStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_URL]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("URL created successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("URL changed successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("URL deleted successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.UrlId == id);
			if (target != null) entity[type] = target;
		}


		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "URLS")
			};
			return ExecuteStoredProcedure("dbo.[sp_Pype_Type_NoPOD]", parameters, true);
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Url", "Url"), true);
		}

        protected override void OnInitialized()
        {
            // Initialize default entities and filters
            entity["add"] = new Url();
            entity["change"] = new Url();
            entity["delete"] = new Url();
            filter["list"] = "****";
            filter["change"] = "****";
            filter["delete"] = "****";

            // Load data for Url entities
            LoadReadResults();
            LoadReadPypeResults();

            // Handle screen change options
            if (!string.IsNullOrEmpty(show))
            {
                switch (show)
                {
                    case "change":
                        if (urlId.HasValue)
                        {
                            AutoFill(urlId.Value, "change"); // Populate fields for specific UrlId
                        }
                        else if (entities.Any())
                        {
                            entity["change"] = entities.First();
                            AutoFill(entity["change"].UrlId, "change"); // Default to first entity
                        }
                        break;

                    case "delete":
                        var deletedEntity = entities.FirstOrDefault(e => e.UrlStatus == "D");
                        if (deletedEntity != null)
                        {
                            entity["delete"] = deletedEntity;
                            AutoFill(deletedEntity.UrlId, "delete"); // Populate fields for the deleted entity
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
                    AutoFill(entity["change"].UrlId, "change");
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

        private async ThreadingTask HandleDirectSelectKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && int.TryParse(directSelectId, out int id))
            {
                // Find the entity by ID and navigate to the change screen
                var selectedEntity = entities.FirstOrDefault(entity => entity.UrlId == id);
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
                new SqlParameter("@URL_status", status)
            };

            return ExecuteStoredProcedure("dbo.[CRUD_Url]", parameters, true);
        }

        private void ApplyTagFilter(ChangeEventArgs e)
        {
            tagFilter = e.Value?.ToString() ?? string.Empty;
            LoadReadResults("*", tagFilter);
        }
    }
}