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
	public partial class Page_Brand
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
		private List<Brand> entities = new List<Brand>();
		private Dictionary<String, Brand> entity = new Dictionary<String, Brand>();
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
				new SqlParameter("@pod", pod),
				new SqlParameter("@brand_code", entity["add"].BrandCode),
				new SqlParameter("@brand_label", entity["add"].BrandLabel),
				new SqlParameter("@brand_image", entity["add"].BrandImage),
				new SqlParameter("@brand_status", entity["add"].BrandStatus),
				new SqlParameter("@brand_cnt_max", entity["add"].BrandCntMax),
				new SqlParameter("@brand_cnt_reg", 0),
				new SqlParameter("@brand_eligibility", entity["add"].BrandEligibility),
				new SqlParameter("@brand_reg_date_closed", entity["add"].BrandRegDateClosed),
				new SqlParameter("@brand_cost", entity["add"].BrandCost),
				new SqlParameter("@brand_guide", entity["add"].BrandGuide),
				new SqlParameter("@brand_role", entity["add"].BrandRole),
				new SqlParameter("@nova_id_fk", entity["add"].NovaIdFk),
				new SqlParameter("@program_id_fk", pod),
				new SqlParameter("@location_id_fk", entity["add"].LocationIdFk),
				new SqlParameter("@channel_alpha", entity["add"].ChannelAlpha),
				new SqlParameter("@channel_beta", entity["add"].ChannelBeta),
				new SqlParameter("@channel_gamma", entity["add"].ChannelGamma)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Brand]", parameters);
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
			return ExecuteStoredProcedure("dbo.[CRUD_Brand]", parameters, true);
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
				entities.Add(new Brand
				{
					BrandId = reader.GetInt32(0),
					BrandCode = reader.GetString(1),
					BrandLabel = reader.GetString(2),
					BrandImage = reader.GetString(3),
					BrandStatus = reader.GetString(4),
					BrandCntMax = reader.GetInt16(5),
					BrandCntReg = reader.GetInt16(6),
					BrandEligibility = reader.GetInt16(7),
					BrandRegDateClosed = reader.GetDateTime(8),
					BrandCost = reader.GetDecimal(9),
					BrandGuide = reader.GetInt32(10),
					BrandRole = reader.GetString(11),
					NovaIdFk = reader.GetInt32(12),
					ProgramIdFk = reader.GetInt32(13),
					LocationIdFk = reader.GetInt32(14),
					ChannelAlpha = reader.GetString(15),
					ChannelBeta = reader.GetString(16),
					ChannelGamma = reader.GetString(17)
				});
			}
			reader.Close();
		}

		/// <summary>Handle events triggered by the change of the change filter select</summary>
		/// <param name="args">Arguments from a filter change event</param>
		private async ThreadingTask OnChangeFilterChanged(ChangeEventArgs args, String type, String status = "*")
		{
			filter[type] = args.Value.ToString();
			var entitiesFiltered = entities;
			if (!entitiesFiltered.Any()) return;
			entity[type] = (entitiesFiltered == null) ? entities.First() : entitiesFiltered.First();
		}

		/// <summary>Change an entity in the database using a stored procedure</summary>
		private void Change()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@id", entity["change"].BrandId),
				new SqlParameter("@pod", pod),
				new SqlParameter("@brand_code", entity["change"].BrandCode),
				new SqlParameter("@brand_label", entity["change"].BrandLabel),
				new SqlParameter("@brand_image", entity["change"].BrandImage),
				new SqlParameter("@brand_status", entity["change"].BrandStatus),
				new SqlParameter("@brand_cnt_max", entity["change"].BrandCntMax),
				new SqlParameter("@brand_cnt_reg", entity["change"].BrandCntReg),
				new SqlParameter("@brand_eligibility", entity["change"].BrandEligibility),
				new SqlParameter("@brand_reg_date_closed", entity["change"].BrandRegDateClosed),
				new SqlParameter("@brand_cost", entity["change"].BrandCost),
				new SqlParameter("@brand_guide", entity["change"].BrandGuide),
				new SqlParameter("@brand_role", entity["change"].BrandRole),
				new SqlParameter("@nova_id_fk", entity["change"].NovaIdFk),
				new SqlParameter("@program_id_fk", pod),
				new SqlParameter("@location_id_fk", entity["change"].LocationIdFk),
				new SqlParameter("@channel_alpha", entity["change"].ChannelAlpha),
				new SqlParameter("@channel_beta", entity["change"].ChannelBeta),
				new SqlParameter("@channel_gamma", entity["change"].ChannelGamma)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Brand]", parameters);
		}

		/// <summary>Delete an entity in the database using a stored procedure</summary>
		private void Delete()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Delete"),
				new SqlParameter("@id", entity["delete"].BrandId),
				new SqlParameter("@status", entity["delete"].BrandStatus)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Brand]", parameters);
		}

		/// <summary>Handle events triggered by entity creation</summary>
		private void OnCreate()
		{
			Create();
			LoadReadResults();
			NotificationService.Notify("Brand created successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity changes</summary>
		private void OnChange()
		{
			Change();
			LoadReadResults();
			NotificationService.Notify("Brand changed successfully!", NotificationType.Success);
		}

		/// <summary>Handle events triggered by entity deletions</summary>
		private void OnDelete()
		{
			Delete();
			LoadReadResults();
			NotificationService.Notify("Brand deleted successfully!", NotificationType.Success);
			show = "list";
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.BrandId == id);
			if (target != null) entity[type] = target;
		}

		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "BRND"),
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
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Brand", "Brand"), true);
		}

		protected override void OnInitialized()
		{
			entity["add"] = new Brand();
			entity["change"] = new Brand();
			entity["delete"] = new Brand();
			filter["list"] = "****";
			filter["change"] = "****";
			filter["delete"] = "****";

			LoadReadResults();
			LoadReadPypeResults();

			if (entities.Any())
			{
				entity["change"] = entities.First();
				AutoFill(entity["change"].BrandId, "change");
			}

			if (entities.Find(e => e.BrandStatus == "D") != null)
			{
				entity["delete"] = entities.Where(e => e.BrandStatus == "D").First();
				AutoFill(entity["delete"].BrandId, "delete");
			}
			show = "list";
		}
	}
}