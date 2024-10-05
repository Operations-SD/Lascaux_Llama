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
	public partial class Page_Guide
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
		private List<Guide> entities = new List<Guide>();
		private Dictionary<String, Guide> entity = new Dictionary<String, Guide>();
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
				//new SqlParameter("@pod", pod),
				new SqlParameter("@label", entity["add"].GuideLabel),
				new SqlParameter("@purpose", entity["add"].GuidePurpose),
				new SqlParameter("@type", entity["add"].GuideType),
				new SqlParameter("@status", entity["add"].GuideStatus),
				new SqlParameter("@image", entity["add"].GuideImage),
				new SqlParameter("@dt_origin", entity["add"].GuideDtOrgin),
				new SqlParameter("@dt_revision", entity["add"].GuideDtRevision),
				new SqlParameter("@nova_id_fk", entity["add"].NovaFk),
				new SqlParameter("@program_fk", entity["add"].ProgramFk)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Guide]", parameters);
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
			return ExecuteStoredProcedure("dbo.[CRUD_Guide]", parameters, true);
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
				entities.Add(new Guide
				{
					GuideId = reader.GetInt32(0),
					GuideLabel = reader.GetString(1),
					GuidePurpose = reader.GetString(2),
					GuideType = reader.GetString(3),
					GuideStatus = reader.GetString(4),
					GuideImage = reader.GetInt32(5),
					GuideDtOrgin = reader.GetDateTime(6),
					GuideDtRevision = reader.GetDateTime(7),
					NovaFk = reader.GetInt32(8),
					ProgramFk = reader.GetInt32(9)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.GuideType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.GuideStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				//new SqlParameter("@pod", pod),
				new SqlParameter("@label", entity["change"].GuideLabel),
				new SqlParameter("@purpose", entity["change"].GuidePurpose),
				new SqlParameter("@type", entity["change"].GuideType),
				new SqlParameter("@status", entity["change"].GuideStatus),
				new SqlParameter("@image", entity["change"].GuideImage),
				new SqlParameter("@dt_origin", entity["change"].GuideDtOrgin),
				new SqlParameter("@dt_revision", entity["change"].GuideDtRevision),
				new SqlParameter("@nova_id_fk", entity["change"].NovaFk),
				new SqlParameter("@program_fk", entity["change"].ProgramFk)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Guide]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].GuideId),
				new SqlParameter("@status", entity["delete"].GuideStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Guide]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("Guide created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("Guide changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("Guide deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.GuideId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "GUID"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Guide", "Guide"), true);
		}

		protected override void OnInitialized()
		{
			entity["add"]    = new Guide();
			entity["change"] = new Guide();
			entity["delete"] = new Guide();
			filter["list"]   = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].GuideId, "change");
			}

			if (entities.Find(e => e.GuideStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.GuideStatus == "D").First();
				AutoFill(entity["delete"].GuideId, "delete");
			}
			show = "list";
		}
	
	}
}