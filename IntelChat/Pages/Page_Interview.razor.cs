using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Data;
using System.Data.SqlClient;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Interview
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		public int? gid { get; set; } = 3; // Temporary: ID for Intel-a-Chat Guide
		public Guide guide;


		[Inject]
		public NotificationService NotificationService { get; set; }
		private short? response = null;
		private short? severity = null;
		private string dispResponse = "-";
		private string dispSeverity = "-";
		private List<Question> questions = new List<Question>();
		private List<Answer> answers = new List<Answer>();
		private int current = 0;
		private Memo memo = new Memo();
		public bool modal = false;
		public string show = "";
		public string name = "";
		public string message = "";
		public string messageType = "";

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
			var connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQuery();
			return null;
		}

		/// *********************************************************************************
		/// ***************************** Load POD Questions  *******************************
		/// *********************************************************************************
		private SqlDataReader? ReadQuestion(string status = "A")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@guide_id", gid),
				new SqlParameter("@question_status", status)
			};

			return ExecuteStoredProcedure("dbo.[sp_Iview_Load]", parameters, true);
		}

		private void LoadQuestion(string status = "A")
		{
			var reader = ReadQuestion(status);
			if (reader == null) return;

			questions.Clear();
			while (reader.Read())
			{
				questions.Add(new Question
				{
					QuestionId = reader.GetInt32(0),
					QuestionText = reader.GetString(1),
					QuestionType = reader.GetString(2),
					QuestionStatus = reader.GetString(3),
					NovaIdFk = reader.GetInt32(5),
				});
			}

			reader.Close();
		}

		private SqlDataReader? ReadAnswer()
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@person_id_fk", pid)
			};

			return ExecuteStoredProcedure("dbo.[CRUD_Answer]", parameters, true);
		}

		/// *********************************************************************************
		/// ******************* Select Response and Severity  *******************************
		/// *********************************************************************************
		private void LoadAnswer()
		{
			var reader = ReadAnswer();
			if (reader == null) return;

			answers.Clear();
			while (reader.Read())
			{
				answers.Add(new Answer
				{
					PersonId = reader.GetInt32(0),
					QuestionId = reader.GetInt32(1),
					AnswerResponse = reader.GetInt16(2),
					AnswerSeverity = reader.GetInt16(3),
					AnswerDtOrgin = reader.GetDateTime(4),
					AnswerDtRevision = reader.GetDateTime(5)
				});
			}

			reader.Close();
		}

		/// *********************************************************************************
		/// ************** Write the answered question to person log  ***********************
		/// *********************************************************************************
		private void CreateAnswer(int response, int severity, int pid, int questionId)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Create"),
				new SqlParameter("@response", response),
				new SqlParameter("@severity", severity),
				new SqlParameter("@person_id_fk", pid),
				new SqlParameter("@question_id_fk", questionId)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Answer]", parameters);
		}

		private void ChangeAnswer(int response, int severity, int pid, int questionId)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Update"),
				new SqlParameter("@response", response),
				new SqlParameter("@severity", severity),
				new SqlParameter("@person_id_fk", pid),
				new SqlParameter("@question_id_fk", questionId)
			};

			ExecuteStoredProcedure("dbo.[CRUD_Answer]", parameters);
		}

		/// *********************************************************************************
		/// ************************* READ Guide Information  *******************************
		/// *********************************************************************************
		private Guide ReadGuide()
		{
			SqlConnection connection = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
			SqlCommand cmd = new SqlCommand("[dbo].[CRUD_Guide]", connection);

			SqlParameter param1 = new SqlParameter("@PROC_action", "Read");
			SqlParameter param2 = new SqlParameter("@id", gid);

			cmd.Parameters.Add(param1);
			cmd.Parameters.Add(param2);

			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.Read())
			{
				guide = new Guide
				{
					GuideId = reader.GetInt32(0),
					GuideLabel = reader.GetString(1),
					GuidePurpose = reader.GetString(2),
					GuideType = reader.GetString(3),
					GuideStatus = reader.GetString(4),
					GuideImage = reader.GetInt32(5),
					GuideDtOrgin = reader.GetDateTime(6),
					GuideDtRevision = reader.GetDateTime(7),
					NovaIdFk = reader.GetInt32(8),
					ProgramIdFk = reader.GetInt32(9)
				};
				reader.Close();
				return guide;
			}
			return null;
		}

		public void HideModal() => modal = false;
		public void ShowModal() => modal = true;
		public void ShowChat() { ShowModal(); show = "chat"; }
		public void ShowMemo() { ShowModal(); show = "memo"; }
		public void ShowInfo() { ShowModal(); show = "info"; }

		/// *********************************************************************************
		/// ********************* Previous or Next Question  *******************************
		/// *********************************************************************************
		private void GoToPreviousQuestion()
		{
			if (current <= 0) return;
			current--;
			dispResponse = "-";
			dispSeverity = "-";
			response = null;
			severity = null;
			LoadPreviousAnswer();
		}

		private void GoToNextQuestion()
		{
			if (current >= questions.Count - 1) return;
			current++;
			dispResponse = "-";
			dispSeverity = "-";
			response = null;
			severity = null;
			LoadPreviousAnswer();
		}

		private void LoadPreviousAnswer()
		{
			Answer? target = answers.Find(x => x.QuestionId == questions[current].QuestionId);
			if (target == null) return;
			ChangeResponse(target.AnswerResponse);
			ChangeSeverity(target.AnswerSeverity);
		}

		/// *********************************************************************************
		/// ******************* Select Response and Severity  *******************************
		/// *********************************************************************************
		private void ChangeResponse(short response)
		{
			if (this.response == response)
			{
				this.response = 0;
				response = 0;
			}
			else
			{
				this.response = response;
			}
			dispResponse = response switch
			{
				1 => "Yes - True",
				2 => "No - False",
				3 => "no opinion / do not know",
				4 => "I do not understand",
				5 => "it is a stupid question",
				_ => ""
			};

			Answer? answer = answers.Find(answer => answer.QuestionId == questions[current].QuestionId);
			if (answer == null) answers.Add(new Answer() { PersonId = pid ?? 0, QuestionId = questions[current].QuestionId, AnswerResponse = response });
			else answer.AnswerResponse = response;
		}

		private void ChangeSeverity(short severity)
		{
			if (this.severity == severity)
			{
				this.severity = 0;
				severity = 0;
			}
			else
			{
				this.severity = severity;
			}
			dispSeverity = severity switch
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

			Answer? answer = answers.Find(answer => answer.QuestionId == questions[current].QuestionId);
			if (answer == null) answers.Add(new Answer() { PersonId = pid ?? 0, QuestionId = questions[current].QuestionId, AnswerSeverity = severity });
			else answer.AnswerSeverity = severity;
		}

		private void Process()
		{
			questions.ForEach(question => {
				Answer? answer = answers.Find(answer => answer.QuestionId == question.QuestionId);
				if (answer == null) return;
				if (!answer.AnswerDtOrgin.Equals(default(DateTime))) ChangeAnswer(answer.AnswerResponse, answer.AnswerSeverity, answer.PersonId, question.QuestionId);
				else CreateAnswer(answer.AnswerResponse, answer.AnswerSeverity, answer.PersonId, question.QuestionId);
			});

			_nav.NavigateTo($"/InterviewResult?pod={pod}&pid={pid}&gid={gid}");
		}

		/// *********************************************************************************
		/// *********************** Navigate to another Page  *******************************
		/// *********************************************************************************
		private void Exit()
		{
			_nav.NavigateTo($"/?pod={pod}&pid={pid}");
		}

		public void NavigateToLascaux()
		{
			_nav.NavigateTo(String.Format("/Lascaux?pod={0}&pid={1}&prevPage={2}&type={3}", pod, pid, "Interview", "Interview"), true);
		}

		protected override void OnInitialized()
		{
		    LoadQuestion();
			LoadAnswer();
			LoadPreviousAnswer();
		}
	}
}