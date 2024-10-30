using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
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

		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; } = "list";
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
		private void LoadReadResults(string status = "*")
		{
			var reader = Read(status);
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
					UrlIdPk = reader.GetInt32(6)
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
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.NounId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "****"),
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
			entity["add"] = new Noun();
			entity["change"] = new Noun();
			entity["delete"] = new Noun();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].NounId, "change");
			}

			if (entities.Find(e => e.NounStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.NounStatus == "D").First();
				AutoFill(entity["delete"].NounId, "delete");
			}
			show = "list";
		}
		
		private bool showPypeIdList = true;
		private string selectedPypeId;
		private List<Pype> filteredPypes = new List<Pype>();

		private void OnPypeIdSelected(ChangeEventArgs e)
		{
			var selectedValue = e.Value?.ToString();

			if (showPypeIdList)
			{
				// Set selected PypeId and filter based on that
				selectedPypeId = selectedValue;

				// Filter the Pype list by the selected PypeId
				filteredPypes = pypes.Where(p => p.PypeId == selectedPypeId).ToList();

				// Switch to showing the Pype Types
				showPypeIdList = false;
			}
			else if (selectedValue == "back")
			{
				// Go back to PypeId list
				showPypeIdList = true;
			}
			else
			{
				// Set the selected PypeType as needed and apply any further logic
				filter["list"] = selectedValue;

				//entities = entities.Where(entity => entity.NounType == selectedValue).ToList();
			}
		
		}
		
	}
}