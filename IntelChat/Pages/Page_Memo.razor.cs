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
	public partial class Page_Memo
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
		private List<Memo> entities = new List<Memo>();
		private Dictionary<String, Memo> entity = new Dictionary<String, Memo>();
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
				new SqlParameter("@memo_person_to", entity["add"].MemoPersonTo),
				new SqlParameter("@memo_person_from", entity["add"].MemoPersonFrom),
				new SqlParameter("@memo_date_time", entity["add"].MemoDateTime),
				new SqlParameter("@memo_priority", entity["add"].MemoPriority),
				new SqlParameter("@memo_pod", entity["add"].GuideIdFk),
				new SqlParameter("@memo_nova", entity["add"].MemoNova),
				new SqlParameter("@memo_channel", entity["add"].MemoChannel),
				new SqlParameter("@memo_type", entity["add"].MemoType),
				new SqlParameter("@memo_status", entity["add"].MemoStatus),
				new SqlParameter("@memo_message", entity["add"].MemoMessage)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters);
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
				new SqlParameter("@memo_status", status),
				new SqlParameter("@memo_pod", pod)
			};
			return ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters, true);
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
				entities.Add(new Memo
				{
					MemoPersonTo = reader.GetInt32(0),
					MemoPersonFrom = reader.GetInt32(1),
					MemoDateTime = reader.GetDateTime(2),
					MemoPriority = (byte)reader.GetInt16(3),
					GuideIdFk = reader.GetInt32(4),
					MemoNova = reader.GetInt32(5),
					MemoChannel = reader.GetString(6),
					MemoType = reader.GetString(7),
					MemoStatus = reader.GetString(8),
					MemoMessage = reader.GetString(9)
					
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.MemoType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.MemoStatus == status);
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
				new SqlParameter("@memo_person_to", entity["change"].MemoPersonTo),
				new SqlParameter("@memo_person_from", entity["change"].MemoPersonFrom),
				new SqlParameter("@memo_date_time", entity["change"].MemoDateTime),
				new SqlParameter("@memo_priority", entity["add"].MemoPriority),
				new SqlParameter("@memo_pod", entity["change"].GuideIdFk),
				new SqlParameter("@memo_nova", entity["change"].MemoNova),
				new SqlParameter("@memo_channel", entity["change"].MemoChannel),
				new SqlParameter("@memo_type", entity["change"].MemoType),
				new SqlParameter("@memo_status", entity["change"].MemoStatus),
				new SqlParameter("@memo_message", entity["change"].MemoMessage)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@memo_person_to", entity["delete"].MemoPersonTo),
				new SqlParameter("@memo_person_from", entity["delete"].MemoPersonFrom),
				new SqlParameter("@memo_date_time", entity["delete"].MemoDateTime),
				new SqlParameter("@status", entity["delete"].MemoStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("Memo created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("Memo changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("Memo deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.MemoChannel == id.ToString());
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "MEMO"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Memo", "Memo"), true);
		}

		protected override void OnInitialized()
		{
			entity["add"] = new Memo();
			entity["change"] = new Memo();
			entity["delete"] = new Memo();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].GuideIdFk, "change");
			}

			if (entities.Find(e => e.MemoStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.MemoStatus == "D").First();
				AutoFill(entity["delete"].GuideIdFk, "delete");
			}
			show = "list";
		}
	
	}
}