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
	public partial class Page_POD
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		public string type = "NOVA"; // *************** Lascaux Case Switch - POD NOVA TASK WORK NOUN VERB QUESTION INTERVIEW
		[Inject]
		public NotificationService NotificationService { get; set; }
		private string? show { get; set; } = "list";
		private List<Pype> pypes = new List<Pype>();
		private List<Pod> entities = new List<Pod>();
		private Dictionary<String, Pod> entity = new Dictionary<String, Pod>();
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
				new SqlParameter("@pod_id", pod),
				new SqlParameter("@pod_label", entity["add"].PodLabel),
				new SqlParameter("@pod_description", entity["add"].PodDescription),
				new SqlParameter("@pod_type", entity["add"].PodPypeDdPods),
				new SqlParameter("@pod_status", entity["add"].PodStatus),
				new SqlParameter("@pod_channel", entity["add"].PodPypeDdChan),
				new SqlParameter("@pod_url_base", entity["add"].PodImage),
				new SqlParameter("@person_id_fk", entity["add"].PersonIdFk),
				new SqlParameter("@location_id_fk", entity["add"].LocationIdFk),
				new SqlParameter("@nova_id_fk", entity["add"].NovaIdFk)
			};
			ExecuteStoredProcedure("dbo.[CRUD_POD]", parameters);
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
				new SqlParameter("@pod_status", status),
				new SqlParameter("@pod_id", pod)
			};
			return ExecuteStoredProcedure("dbo.[CRUD_POD]", parameters, true);
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
				entities.Add(new Pod
				{
					PodId = reader.GetInt32(0),
					PodLabel = reader.GetString(1),
					PodDescription = reader.GetString(2),
					PodPypeDdPods = reader.GetString(3),
					PodStatus = reader.GetString(4),
					PodPypeDdChan = reader.GetString(5),
					PodImage = reader.GetString(6),
					PersonIdFk = reader.GetInt32(7),
					LocationIdFk = reader.GetInt32(8),
					NovaIdFk = reader.GetInt32(9)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = (filter[type] == "****") ? entities : entities.Where(entity => entity.PodPypeDdPods == filter[type]);
			if (!entitiesFiltered.Any()) return;
			if (status != "*") entitiesFiltered = entitiesFiltered.Where(entity => entity.PodStatus == status);
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@pod_id", pod),
				new SqlParameter("@pod_label", entity["change"].PodLabel),
				new SqlParameter("@pod_description", entity["change"].PodDescription),
				new SqlParameter("@pod_type", entity["change"].PodPypeDdPods),
				new SqlParameter("@pod_status", entity["change"].PodStatus),
				new SqlParameter("@pod_channel", entity["change"].PodPypeDdChan),
				new SqlParameter("@pod_url_base", entity["change"].PodImage),
				new SqlParameter("@person_id_fk", entity["add"].PersonIdFk),
				new SqlParameter("@location_id_fk", entity["change"].LocationIdFk),
				new SqlParameter("@nova_id_fk", entity["add"].NovaIdFk)
			};
			ExecuteStoredProcedure("dbo.[CRUD_POD]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].PodId),
				new SqlParameter("@status", entity["delete"].PodStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_POD]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("POD created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("POD changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("POD deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.PodId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "PODS"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Pod", "Pod"), true);
		}










		protected override void OnInitialized()
		{
			entity["add"] = new Pod();
			entity["change"] = new Pod();
			entity["delete"] = new Pod();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].PodId, "change");
			}

			if (entities.Find(e => e.PodStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.PodStatus == "D").First();
				AutoFill(entity["delete"].PodId, "delete");
			}
			show = "list";
		}
	
	}
}