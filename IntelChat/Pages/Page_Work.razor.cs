using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Task = IntelChat.Models.Task;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Work
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		public string type = "work"; // *************** Lascaux Case Switch - POD NOVA TASK Work NOUN VERB QUESTION INTERVIEW
		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; } = "list";
		private List<Pype> pypes = new List<Pype>();
		private List<Work> entities = new List<Work>();
		private Dictionary<String, Work> entity = new Dictionary<String, Work>();
		private Dictionary<String, String> filter = new Dictionary<String, String>();
		private string pageName = "Work";

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
				new SqlParameter("@Work_label32", entity["add"].WorkLabel32),
				new SqlParameter("@Work_type", entity["add"].WorkType),
				new SqlParameter("@Work_status", entity["add"].WorkStatus),
				new SqlParameter("@Work_level", entity["add"].WorkLevel),
				new SqlParameter("@Work_description", entity["add"].WorkDescription),
				new SqlParameter("@Work_duration", entity["add"].WorkDuration),
				new SqlParameter("@Work_start", entity["add"].WorkStart),
				new SqlParameter("@Work_finish", entity["add"].WorkFinish),
				new SqlParameter("@Work_entry_data", entity["add"].WorkEntryData),
				new SqlParameter("@Person_ID_FK", entity["add"].PersonIdFk),
				new SqlParameter("@NOVA_ID_FK", entity["add"].NovaIdFk),
				new SqlParameter("@Task_ID_FK", entity["add"].TaskIdFk),
				new SqlParameter("@POD_Counter_Work", entity["add"].PodCounterWork),
				new SqlParameter("@Work_start_date", entity["add"].WorkStartDate),
				new SqlParameter("@Work_finish_date", entity["add"].WorkFinishDate)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Work]", parameters);
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
			return ExecuteStoredProcedure("dbo.[CRUD_Work]", parameters, true);
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
				entities.Add(new Work
				{
					WorkId = reader.GetInt32(0),
					WorkLabel32 = reader.GetString(1),
					WorkType = reader.GetString(2),
					WorkStatus = reader.GetString(3),
					WorkLevel = reader.GetInt16(4),
					WorkDescription = reader.GetString(5),
					WorkDuration = reader.GetInt16(6),
					WorkStart = reader.GetInt16(7),
					WorkFinish = reader.GetInt16(8),
					WorkEntryData = reader.GetDateTime(9),
					PersonIdFk = reader.GetInt32(10),
					NovaIdFk = reader.GetInt32(11),
					TaskIdFk = reader.GetInt32(12),
					PodCounterWork = reader.GetInt16(13),
					WorkStartDate = reader.GetDateTime(14),
					WorkFinishDate = reader.GetDateTime(15)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.WorkType == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.WorkStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@id", entity["change"].WorkId),
				new SqlParameter("@pod", pod),
				new SqlParameter("@Work_label32", entity["change"].WorkLabel32),
				new SqlParameter("@Work_type", entity["change"].WorkType),
				new SqlParameter("@Work_status", entity["change"].WorkStatus),
				new SqlParameter("@Work_level", entity["change"].WorkLevel),
				new SqlParameter("@Work_description", entity["change"].WorkDescription),
				new SqlParameter("@Work_duration", entity["change"].WorkDuration),
				new SqlParameter("@Work_start", entity["change"].WorkStart),
				new SqlParameter("@Work_finish", entity["change"].WorkFinish),
				new SqlParameter("@Work_entry_data", entity["change"].WorkEntryData),
				new SqlParameter("@Person_ID_FK", entity["change"].PersonIdFk),
				new SqlParameter("@NOVA_ID_FK", entity["change"].NovaIdFk),
				new SqlParameter("@Task_ID_FK", entity["change"].TaskIdFk),
				new SqlParameter("@POD_Counter_Work", entity["change"].PodCounterWork),
				new SqlParameter("@Work_start_date", entity["change"].WorkStartDate),
				new SqlParameter("@Work_finish_date", entity["change"].WorkFinishDate)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Work]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].WorkId),
				new SqlParameter("@status", entity["delete"].WorkStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Work]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("Work created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("Work changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("Work deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.WorkId == id);
			if (target != null) entity[type] = target;
		}







		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "WORK"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Work", "Work"), true);
		}

		protected override void OnInitialized()
		{
			entity["add"] = new Work();
			entity["change"] = new Work();
			entity["delete"] = new Work();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();
			entity["add"].WorkEntryData = DateTime.Today;
			entity["add"].WorkStartDate = DateTime.Today;
			entity["add"].WorkFinishDate = DateTime.Today;

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].WorkId, "change");
			}

			if (entities.Find(e => e.WorkStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.WorkStatus == "D").First();
				AutoFill(entity["delete"].WorkId, "delete");
			}
			show = "list";
		}


		public void NavigateToLascaux()
		{
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Interview", "Interview"), true);
		}






	}
}