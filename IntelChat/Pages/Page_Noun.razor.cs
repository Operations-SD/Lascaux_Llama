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
	public partial class Page_Noun
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public string? prevPage { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public int? nounId { get; set; } // Noun ID for navigation

		[Parameter]
		[SupplyParameterFromQuery]
		public string? show { get; set; } // Tab to display

		[Parameter]
		[SupplyParameterFromQuery]
		public string? type { get; set; } // Type of noun (Subject or Object)
		
		public string? StoredNoun { get; set; }

		public string ImageUrl { get; set; } = string.Empty;

		[Inject]
		public NotificationService NotificationService { get; set; }

		private List<Pype> pypes = new List<Pype>();
		private List<Noun> entities = new List<Noun>();
		private Dictionary<String, Noun> entity = new Dictionary<String, Noun>();
		private Dictionary<String, String> filter = new Dictionary<String, String>();
		private string pageName = "Noun";

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
				new SqlParameter("@label", entity["add"].NounLabel),
				new SqlParameter("@desc", entity["add"].NounDescription),
				new SqlParameter("@type", entity["add"].NounType),
				new SqlParameter("@status", entity["add"].NounStatus),
				new SqlParameter("@pod", pod)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Noun]", parameters);
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

			return ExecuteStoredProcedure("dbo.[CRUD_Noun]", parameters, true);	
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
				entities.Add(new Noun
				{
					NounId = reader.GetInt32(0),
					NounLabel = reader.GetString(1),
					NounDescription = reader.GetString(2),
					NounType = reader.GetString(3),
					NounStatus = reader.GetString(4),
					PodIdFk = reader.GetInt32(5),
					UrlIdPk = reader.GetInt32(6),
					NounTag = reader.GetString(7)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private void OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(noun => noun.NounType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.NounStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
					new SqlParameter("@PROC_action", "Update"),
					new SqlParameter("@id", entity["change"].NounId),
					new SqlParameter("@label", entity["change"].NounLabel),
					new SqlParameter("@desc", entity["change"].NounDescription),
					new SqlParameter("@type", entity["change"].NounType),
					new SqlParameter("@status", entity["change"].NounStatus),
					new SqlParameter("@url_id_pk", entity["change"].UrlIdPk),
					new SqlParameter("@tag", entity["change"].NounTag),
					new SqlParameter("@pod", pod)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Noun]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].NounId),
				new SqlParameter("@status", entity["delete"].NounStatus)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Noun]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			entities.Add(entity["add"]);
			NotificationService.Notify("Noun created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			entities.Remove(entities.Find(e => e.NounId == entity["change"].NounId));
			entities.Add(entity["change"]);
			NotificationService.Notify("Noun changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			entities.Remove(entities.Find(e => e.NounId == entity["delete"].NounId));
			NotificationService.Notify("Noun deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, string type)
		{
			var target = entities.FirstOrDefault(e => e.NounId == id);
			if (target != null)
			{
				entity[type] = target;

				//Fetch the image URL when selecting a noun
				_ = LoadImageUrlAsync(target.UrlIdPk);
			}
		}


		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		///

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
			StoredNoun = result.Value.Noun;
			
			
			var reader = ReadPype(StoredNoun);
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

		public void NavigateToLascaux(bool isSubject)
		{
			string position = isSubject ? "Subject" : "Object";
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Noun", $"Noun{position}"), true);
		}

		protected override void OnInitialized()
		{
			// Initialize default entities and filters
			entity["add"] = new Noun();
			entity["change"] = new Noun();
			entity["delete"] = new Noun();
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
						if (nounId.HasValue)
						{
							AutoFill(nounId.Value, "change"); // Populate fields for specific NounId
						}
						else if (entities.Any())
						{
							entity["change"] = entities.First();
							AutoFill(entity["change"].NounId, "change"); // Default to first entity
						}
						break;

					case "delete":
						var deletedEntity = entities.FirstOrDefault(e => e.NounStatus == "D");
						if (deletedEntity != null)
						{
							entity["delete"] = deletedEntity;
							AutoFill(deletedEntity.NounId, "delete"); // Populate fields for the deleted entity
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
					AutoFill(entity["change"].NounId, "change");
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
				var selectedEntity = entities.FirstOrDefault(entity => entity.NounId == id);
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

			return ExecuteStoredProcedure("dbo.[CRUD_Noun]", parameters, true);
		}

		private void ApplyTagFilter(ChangeEventArgs e)
		{
			tagFilter = e.Value?.ToString() ?? string.Empty;
			LoadReadResults("*", tagFilter);
		}

		private string GetImageUrl(int urlId)
		{
			string imageUrl = string.Empty;

			using (var connection = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
			{
				using (var command = new SqlCommand("GetImageUrlById", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UrlId", urlId);
					connection.Open();
					var result = command.ExecuteScalar();
					if (result != null)
					{
						imageUrl = result.ToString();
					}
				}
			}

			return imageUrl;
		}

		private async ThreadingTask LoadImageUrlAsync(int urlId)
		{
			if (urlId > 0)
			{
				ImageUrl = GetImageUrl(urlId);
				await InvokeAsync(StateHasChanged); // ✅ Ensure the UI updates
			}
		}

        private async ThreadingTask OnUrlIdChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int newUrlId))
            {
                entity["change"].UrlIdPk = newUrlId;
                await LoadImageUrlAsync(newUrlId);
            }
        }
    }
}