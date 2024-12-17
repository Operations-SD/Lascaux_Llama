using IntelChat.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Interview_Result
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? gid { get; set; }

		private List<InterviewFeature> features = new List<InterviewFeature>();
		private List<XnovaDictionaryTask> tasks = new List<XnovaDictionaryTask>();
		private List<ReportTaskWork>    reports = new List<ReportTaskWork>();

		Dictionary<string, int> scores = new Dictionary<string, int>();
		private (string Type, string Label, string Desc) result = ("", "", "");
		private int taskId;
		private string tab = "task";



		// ********************************* temporary internal file / TABLE *****************************
		private class InterviewFeature
		{
			public string QuestionType { get; set; } = null!;
			public short AnswerResponse { get; set; }
			public short AnswerSeverity { get; set; }
			public string QuestionText { get; set; } = null!;
			public string PypeLabel { get; set; } = null!;
			public string PypeDesc { get; set; } = null!;
		}

		/// <summary>Executes a stored procedure and (optionally) returns a reader for its results</summary>
		/// <param name="procedure">Name of the stored procedure</param>
		/// <param name="parameters">List of arguments for the stored procedure</param>
		/// <param name="reader">Whether a reader for the stored procedure results should be returned</param>
		/// <returns>
		/// Reader for the results of the stored procedure, if reader = true    if reader = false null
		/// </returns>


		// *********************************************************************************** setup READER *******************
		private SqlDataReader? ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQuery();
			return null;
		}



		private SqlDataReader? ReadInterviewResults()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pid", pid),
				new SqlParameter("@guide_id", gid)
			};

			// ANSWERS TO QUESTIONS WITH TYPES THAT ARE NOT IN PYPE TABLE WILL NOT BE LOADED BY DESIGN
			return ExecuteStoredProcedure("dbo.[sp_load_interview_results]", parameters, true);
		}



		private SqlDataReader? ReadTasks()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@PROC_Input_Filter", "****")
			};

			return ExecuteStoredProcedure("dbo.[sp_nova_Dictionary_Task]", parameters, true);
		}



		private SqlDataReader? ReadReportTaskWorks()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@PROC_Input_Filter", "****")
			};

			return ExecuteStoredProcedure("dbo.[sp_report_Task_Work]", parameters, true);
		}



		// *******************************************************  LOAD OnInitialized **********
		private void LoadInterviewResults()
		{
			var reader = ReadInterviewResults();
			if (reader == null) return;

			features.Clear();
			while (reader.Read())
			{
				features.Add(new InterviewFeature   // ******** previously defined temporary file / TABLE ******** 
				{
					QuestionType = reader.GetString(0),
					AnswerResponse = reader.GetInt16(1),
					AnswerSeverity = reader.GetInt16(2),
					QuestionText = reader.GetString(3),
					PypeLabel = reader.GetString(4),
					PypeDesc = reader.GetString(5)
				});
			}

			reader.Close();
		}



		// ******************** Use READER to **** LOAD VIEW via MODEL **** within NnetDbContext ******** OnInitialized **********
		private void LoadTask()
		{
			var reader = ReadTasks();
			if (reader == null) return;

			tasks.Clear();
			while (reader.Read())
			{
				tasks.Add(new XnovaDictionaryTask   // ******** MODEL ******** 
				{
					From = reader.GetString(0),
					N = reader.GetInt32(1),
					Ntype = reader.GetString(2),
					NovaDescription = reader.GetString(3),
					S = reader.GetInt32(4),
					Subject = reader.GetString(5),
					V = reader.GetInt32(6),
					Verb = reader.GetString(7),
					O = reader.GetInt32(8),
					Object = reader.GetString(9),
					Sdesc = reader.GetString(10),
					Vdesc = reader.GetString(11),
					Odesc = reader.GetString(12),
					P = reader.GetInt32(13),
					Pdesc = !reader.IsDBNull(14) ? reader.GetString(14) : "",
					X = !reader.IsDBNull(15) ? reader.GetInt32(15) : 0,
					Xtype = !reader.IsDBNull(16) ? reader.GetString(16) : "",
					Xdesc = !reader.IsDBNull(17) ? reader.GetString(17) : "",
				});
			}

			reader.Close();
		}



		// ******************** Use READER to **** LOAD VIEW via MODEL **** within NnetDbContext ******** OnInitialized **********
		private void LoadReportTaskWorks()
		{
			var reader = ReadReportTaskWorks();
			if (reader == null) return;

			reports.Clear();
			while (reader.Read())
			{
				reports.Add(new ReportTaskWork   // ******** MODEL ******** 
				{
					TaskId = reader.GetInt32(0),
					TaskLabel32 = reader.GetString(1),
					TaskType = reader.GetString(2),
					WorkId = reader.GetInt32(3),
					WorkLabel32 = reader.GetString(4),
					WorkType = reader.GetString(5),
					WorkDescription = reader.GetString(6),
					PodIdFk = reader.GetInt32(7)
				});
			}

			reader.Close();
		}


		//*************************** processing text labels *******************
		private string GetResponseText(short response)
		{
			return response switch
			{
				1 => "Yes - True",
				2 => "No - False",
				3 => "No opinion / do not know",
				4 => "I do not understand",
				5 => "It is a stupid question",
				_ => ""
			};
		}

		private string GetSeverityText(short severity)
		{
			return severity switch
			{
				7 => "purpose of my life",
				6 => "important to me",
				5 => "a concern of mine",
				4 => "normal - average",
				3 => "slightly concerned",
				2 => "not important",
				1 => "do not care at all",
				_ => ""
			};
		}


		// ******************** multiply by 1,-1,0 *******************
		private short GetScore(short response, short severity)
		{
			return response switch
			{
				1 => severity,
				2 => (short) -severity,
				_ => 0
			};
		}



		// *******************************************************  Last Process **** LOAD OnInitialized **********
		private void LoadResult()
		{
			scores.Clear();
			features.ForEach(feature => {
				if (scores.ContainsKey(feature.QuestionType)) scores[feature.QuestionType]
					+= GetScore(feature.AnswerResponse, feature.AnswerSeverity);
				else scores.Add(feature.QuestionType, GetScore(feature.AnswerResponse, feature.AnswerSeverity));
			});
			scores = scores.OrderByDescending(score => score.Value).ToDictionary(score => score.Key, score => score.Value);

			result.Type = scores.MaxBy(score => score.Value).Key;
			var feature = features.Find(feature => feature.QuestionType == result.Type);
			if (feature == null) return;
			result.Label = feature.PypeLabel;
			result.Desc = feature.PypeDesc;
		}

		private void Exit()
		{
			_nav.NavigateTo($"/?pod={pod}&pid={pid}");
		}


		// *******************************************************  LOAD OnInitialized **********
		protected override void OnInitialized()
		{
			LoadInterviewResults();
			LoadTask();
			LoadReportTaskWorks();
			LoadResult();
		}
	}
}