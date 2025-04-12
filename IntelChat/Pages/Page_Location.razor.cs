using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ThreadingLocation = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Location
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public int? locationId { get; set; } // Task ID for navigation

		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; }
		private List<Pype> pypes = new List<Pype>();
		private List<Location> entities = new List<Location>();
		private Dictionary<String, Location> entity = new Dictionary<String, Location>();
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
				new SqlParameter("@location_label16", entity["add"].LocationLabel16),
				new SqlParameter("@location_type", entity["add"].LocationType),
				new SqlParameter("@location_status", entity["add"].LocationStatus),
				new SqlParameter("@location_desc", entity["add"].LocationDesc),
				new SqlParameter("@location_time_zone", entity["add"].LocationTimeZone),
				new SqlParameter("@location_storage", entity["add"].LocationStorage),
				new SqlParameter("@location_png", entity["add"].LocationPng),
				new SqlParameter("@location_finder", entity["add"].LocationFinder),
				new SqlParameter("@latitude", entity["add"].Latitude),
				new SqlParameter("@longitude", entity["add"].Longitude),
				new SqlParameter("@brain_fk", entity["add"].BrainFk),
				new SqlParameter("@license_fk", entity["add"].LicenseFk),
				new SqlParameter("@pod", pod),
				new SqlParameter("@program_fk", pod),
				new SqlParameter("@person_fk_admn", entity["add"].PersonFkAdmn),
				new SqlParameter("@person_fk_engr", entity["add"].PersonFkAdmn),
				new SqlParameter("@person_fk_xprt", entity["add"].PersonFkXprt)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Location]", parameters);
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
				new SqlParameter("@location_status", status),
				new SqlParameter("@pod", pod)
			};
			return ExecuteStoredProcedure("dbo.[CRUD_Location]", parameters, true);
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
				entities.Add(new Location
				{
					LocationId = reader.GetInt32(0),
					LocationLabel16 = reader.GetString(1),
					LocationType = reader.GetString(2),
					LocationStatus = reader.GetString(3),
					LocationDesc = reader.GetString(4),
					LocationTimeZone = reader.GetInt32(5),
					LocationStorage = reader.GetString(6),
					LocationPng = reader.GetString(7),
					LocationFinder = reader.GetString(8),
					Latitude = reader.GetFloat(9),
					Longitude = reader.GetFloat(10),
					BrainFk = reader.GetInt32(11),
					LicenseFk = reader.GetInt32(12),
					PodFk = reader.GetInt32(13),
					ProgramIdFk = reader.GetInt32(14),
					PersonFkAdmn = reader.GetInt32(15),
					PersonFkEngr = reader.GetInt32(16),
					PersonFkXprt = reader.GetInt32(17),
					NovaIdFk = reader.GetInt32(19),
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingLocation OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.LocationType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.LocationStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
					new SqlParameter("@PROC_action", "Update"),
					new SqlParameter("@id", entity["change"].LocationId),
					new SqlParameter("@pod", pod),
					new SqlParameter("@location_label16", entity["change"].LocationLabel16),
					new SqlParameter("@location_type", entity["change"].LocationType),
					new SqlParameter("@location_status", entity["change"].LocationStatus),
					new SqlParameter("@location_desc", entity["change"].LocationDesc),
					new SqlParameter("@location_time_zone", entity["change"].LocationTimeZone),
					new SqlParameter("@location_storage", entity["change"].LocationStorage),
					new SqlParameter("@location_png", entity["change"].LocationPng),
					new SqlParameter("@location_finder", entity["change"].LocationFinder),
					new SqlParameter("@latitude", entity["change"].Latitude),
					new SqlParameter("@longitude", entity["change"].Longitude),
					new SqlParameter("@brain_fk", entity["change"].BrainFk),
					new SqlParameter("@license_fk", entity["change"].LicenseFk),
					new SqlParameter("@program_fk", pod),
					new SqlParameter("@person_fk_admn", entity["change"].PersonFkAdmn),
					new SqlParameter("@person_fk_engr", entity["change"].PersonFkAdmn),
					new SqlParameter("@person_fk_xprt", entity["change"].PersonFkXprt),
					new SqlParameter("@location_tag", entity["change"].LocationTag)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Location]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].LocationId),
				new SqlParameter("@location_status", entity["delete"].LocationStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Location]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("Location created successfully!", NotificationType.Success);
			LoadReadResults();
			LoadReadPypeResults();
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("Location changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("Location deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.LocationId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "LOCS"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Location", "Location"), true);
		}

		protected override void OnInitialized()
		{

			entity["add"] = new Location();
			entity["change"] = new Location();
			entity["delete"] = new Location();
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
						if (locationId.HasValue)
						{
							AutoFill(locationId.Value, "change"); // Populate fields for specific NounId
						}
						else if (entities.Any())
						{
							entity["change"] = entities.First();
							AutoFill(entity["change"].LocationId, "change"); // Default to first entity
						}
						break;

					case "delete":
						var deletedEntity = entities.FirstOrDefault(e => e.LocationStatus == "D");
						if (deletedEntity != null)
						{
							entity["delete"] = deletedEntity;
							AutoFill(deletedEntity.LocationId, "delete"); // Populate fields for the deleted entity
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
					AutoFill(entity["change"].LocationId, "change");
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
				var selectedEntity = entities.FirstOrDefault(entity => entity.LocationId == id);
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
						new SqlParameter("@location_status", status),
						new SqlParameter("@pod", pod)
					};

			return ExecuteStoredProcedure("dbo.[CRUD_Location]", parameters, true);
		}

		private void ApplyTagFilter(ChangeEventArgs e)
		{
			tagFilter = e.Value?.ToString() ?? string.Empty;
			LoadReadResults("*", tagFilter);
		}
	}
}