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
	public partial class Menu_user
	{
		/*
			************************************************************************************
			******************************** INPUT PARAMETERS **********************************
			************************************************************************************
		 */
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		private Guide selectedGuide;

        private List<Guide> entities = new List<Guide>();
		private string guideIdSelection = "";

		protected override void OnInitialized()
		{
			LoadReadResults();
		}

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
					GuideLabel = reader.GetString(1)
				});
			}
			reader.Close();
		}


        private void HandleGuideChange(ChangeEventArgs e)
        {
            guideIdSelection = e.Value?.ToString();

            if (!string.IsNullOrEmpty(guideIdSelection))
            {
                Console.WriteLine($"Selected GuideId: {guideIdSelection}");

                // Loop through entities to find the matching GuideId
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].GuideId.ToString() == guideIdSelection)
                    {
						selectedGuide = entities[i];
                        //Console.WriteLine($"Matched Guide at index: {selectedGuideIndex}");
                        break;
                    }
                }
            }
           
        }

    }
}