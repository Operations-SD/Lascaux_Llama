using IntelChat.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Reflection;
using ThreadingTask = System.Threading.Tasks.Task;
using Task = IntelChat.Models.Task;
using Microsoft.IdentityModel.Tokens;
using iTextSharp.text.pdf;
using iTextSharp.text;

using System.IO;
using System.Collections.Generic;
using System.Linq;



namespace IntelChat.Pages
{
	public partial class Page_Grid
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public string? entity { get; set; }
		public string table = "----";
		public bool loading = false;
		public bool hasType = false;
		public string typeFilter = "";
		public List<string> filters = new List<string>
		{
			"Answer",
			"Brand",
			"Guide",
			"Interview",
			"Location",
			"Memo",
			"Noun",
			"NOVA",
			"Person",
			"POD",
			"Pype",
			"Question",
			"Registration",
			"Task",
			"URL",
			"Verb",
			"Work"
		};
		public List<Answer> answers = new List<Answer>();
		public List<Brand> brands = new List<Brand>();
		public List<Guide> guides = new List<Guide>();
		public List<Interview> interviews = new List<Interview>();
		public List<Location> locations = new List<Location>();
		public List<Memo> memos = new List<Memo>();
		public List<Noun> nouns = new List<Noun>();
		public List<Nova> novas = new List<Nova>();
		public List<Person> persons = new List<Person>();
		public List<Pod> pods = new List<Pod>();
		public List<Pype> pypes = new List<Pype>();
		public List<Question> questions = new List<Question>();
		public List<Registration> registrations = new List<Registration>();
		public List<Task> tasks = new List<Task>();
		public List<Url> urls = new List<Url>();
		public List<Verb> verbs = new List<Verb>();
		public List<Work> works = new List<Work>();

        /// <summary>Executes a stored procedure and (optionally) returns a reader for its results</summary>
        /// <param name="procedure">Name of the stored procedure</param>
        /// <param name="parameters">List of arguments for the stored procedure</param>
        /// <param name="reader">Whether a reader for the stored procedure results should be returned</param>
        /// <returns>
        /// Reader for the results of the stored procedure, if reader = true
        /// null, if reader = false
        /// </returns>
        /// 


        /// ********************************************************** 
        /// ********************************************************** Set-up Stored Procedure READ
        /// ********************************************************** 


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

        /// <summary>Reads entities from the database using a stored procedure</summary>
        /// <param name="status">Status of the entities that will be read</param>
        /// <returns>Reader for the entities that were read from the database</returns>


        /// ********************************************************** 
        /// ********************************************************** Common Shared Stored Procedure READ
        /// ********************************************************** 

        private SqlDataReader? Read(string sp, string table)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@table", table)
			};

			return ExecuteStoredProcedure($"dbo.[{sp}]", parameters, true);
		}

		/// <summary>Load entities from the database into a list </summary>
		/// <param name="status">Status of the entities that will be loaded</param>




        /// ********************************************************** 
		/// ********************************************************** Start Local Stored Procedure LOAD ANSWERS
		/// ********************************************************** 
		private void LoadAnswers(string sp, string table)
		{
			var reader = Read(sp, table);
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

        /// ********************************************************** 
        /// ********************************************************** End of Local Stored Procedure LOAD ANSWERS 
        /// ********************************************************** 


        private void LoadBrands(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			brands.Clear();
			while (reader.Read())
			{
				brands.Add(new Brand
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

		private void LoadGuides(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			guides.Clear();
			while (reader.Read())
			{
				guides.Add(new Guide
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
				});
			}
			reader.Close();
		}

		private void LoadInterviews(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			interviews.Clear();
			while (reader.Read())
			{
				interviews.Add(new Interview
				{
					GuideId = reader.GetInt32(0),
					QuestionId = reader.GetInt32(1),
					InterviewSeq = reader.GetInt16(2),
					InterviewStatus = reader.GetString(3)
				});
			}
			reader.Close();
		}

		private void LoadLocations(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			locations.Clear();
			while (reader.Read())
			{
				locations.Add(new Location
				{
					LocationId = reader.GetInt32(0),
					LocationLabel16 = reader.GetString(1),
					LocationType = reader.GetString(2),
					LocationStatus = reader.GetString(3),
					LocationDesc = reader.GetString(4),
					LocationTimeZone = reader.GetInt32(5),
					LocationStorage = reader.GetString(6),
					LocationPng = reader.GetString(7),
					LocationFinder = reader.GetString(8),
					Latitude = reader.GetFloat(9),
					Longitude = reader.GetFloat(10),
					BrainFk = reader.GetInt32(11),
					LicenseFk = reader.GetInt32(12),
					PodFk = reader.GetInt32(13),
					ProgramIdFk = reader.GetInt32(14),
					PersonFkAdmn = reader.GetInt32(15),
					PersonFkEngr = reader.GetInt32(16),
					PersonFkXprt = reader.GetInt32(17)
				});
			}
			reader.Close();
		}

		private void LoadMemos(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			memos.Clear();
			while (reader.Read())
			{
				memos.Add(new Memo
				{
					MemoPersonTo = reader.GetInt32(0),
					MemoPersonFrom = reader.GetInt32(1),
					MemoDateTime = reader.GetDateTime(2),
					MemoPriority = reader.GetByte(3),
					GuideIdFk = reader.GetInt32(4),
					MemoNova = reader.GetInt32(5),
					MemoChannel = reader.GetInt32(6),
					MemoType = reader.GetString(7),
					MemoStatus = reader.GetString(8),
					MemoMessage = reader.GetString(9)
				});
			}
			reader.Close();
		}

		private void LoadNouns(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			nouns.Clear();
			while (reader.Read())
			{
				nouns.Add(new Noun
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

		private void LoadNovas(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			novas.Clear();
			while (reader.Read())
			{
				novas.Add(new Nova
				{
					NovaId = reader.GetInt32(0),
					NovaDescription = reader.GetString(1),
					NovaType = reader.GetString(2),
					NovaStatus = reader.GetString(3),
					NovaChannel = reader.GetInt32(4),
					NovaSubject = reader.GetInt32(5),
					NovaAction = reader.GetInt32(6),
					NovaObject = reader.GetInt32(7),
					NovaDatetime = reader.GetDateTime(8),
					PodIdFk = reader.GetInt32(9),
					PersonIdFk = reader.GetInt32(10),
					NovaPrioriy = reader.GetInt16(11),
					NovaLabel = reader.GetString(12)
				});
			}
			reader.Close();
		}

		private void LoadPersons(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			persons.Clear();
			while (reader.Read())
			{
				persons.Add(new Person()
				{
					PersonId = reader.GetInt32(0),
					PersonFirst = reader.GetString(1),
					PersonLast = reader.GetString(2),
					PersonLabel = reader.GetString(3),
					PersonPypeDdMyme = reader.GetString(4),
					PersonStatus = reader.GetString(5),
					PersonPypeDdRole = reader.GetString(6),
					PersonDatetime = reader.GetDateTime(7),
					PodIdFk = reader.GetInt32(8),
					LocationIdFk = reader.GetInt32(9)
				});
			}
			reader.Close();
		}

		private void LoadPods(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			pods.Clear();
			while (reader.Read())
			{
				pods.Add(new Pod
				{
					PodId = reader.GetInt32(0),
					PodLabel = reader.GetString(1),
					PodDescription = reader.GetString(2),
					PodType = reader.GetString(3),
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

		private void LoadPypes(string sp, string table)
		{
			var reader = Read(sp, table);
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

		private void LoadQuestions(string sp, string table)
		{
			var reader = Read(sp, table);
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
					NovaIdFk = reader.GetInt32(4),
				});
			}
			reader.Close();
		}

		private void LoadRegistrations(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			registrations.Clear();
			while (reader.Read())
			{
				registrations.Add(new Registration()
				{
					RegistrationId = reader.GetInt32(0),
					RegistrationUsername = reader.GetString(1),
					RegistrationPassword = reader.GetString(2),
					RegistrationEmail = reader.GetString(3),
					RegistrationStatus = reader.GetString(4),
					PersonIdFk = reader.GetInt32(5)
				});
			}
			reader.Close();
		}

		private void LoadTasks(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			tasks.Clear();
			while (reader.Read())
			{
				tasks.Add(new Task
				{
					TaskId = reader.GetInt32(0),
					TaskLabel32 = reader.GetString(1),
					TaskType = reader.GetString(2),
					TaskStatus = reader.GetString(3),
					TaskPlatue = reader.GetInt16(4),
					TaskDescription = reader.GetString(5),
					TaskDuration = reader.GetInt16(6),
					TaskStartDate = reader.GetDateTime(7),
					TaskFinishDate = reader.GetDateTime(8),
					TaskEntryDate = reader.GetDateTime(9),
					TaskPrevious = reader.GetInt32(10),
					PersonIdFk = reader.GetInt32(11),
					NovaIdFk = reader.GetInt32(12),
					NounIdFk = reader.GetInt32(13),
					PodIdFk = reader.GetInt32(14),
					TaskSeq = reader.GetInt16(15),
					TaskParent = reader.GetInt32(16)
				});
			}
			reader.Close();
		}

		private void LoadUrls(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			urls.Clear();
			while (reader.Read())
			{
				urls.Add(new Url
				{
					UrlId = reader.GetInt32(0),
					UrlLabel = reader.GetString(1),
					UrlDescription = reader.GetString(2),
					UrlType = reader.GetString(3),
					UrlStatus = reader.GetString(4),
					UrlCloud = reader.GetString(5)
				});
			}
			reader.Close();
		}

		private void LoadVerbs(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			verbs.Clear();
			while (reader.Read())
			{
				verbs.Add(new Verb
				{
					VerbId = reader.GetInt32(0),
					VerbLabel = reader.GetString(1),
					VerbDescription = reader.GetString(2),
					VerbType = reader.GetString(3),
					VerbStatus = reader.GetString(4),
					PodIdFk = reader.GetInt32(5),
					UrlIdPk = reader.GetInt32(6)
				});
			}
			reader.Close();
		}

		private void LoadWorks(string sp, string table)
		{
			var reader = Read(sp, table);
			if (reader == null) return;

			works.Clear();
			while (reader.Read())
			{
				works.Add(new Work
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

		public async void DisplayTable(ChangeEventArgs e)
		{
			var _table = e.Value?.ToString() ?? "";
			loading = true;
			switch (_table)
			{
				case "Answer":
					if (answers.IsNullOrEmpty()) LoadAnswers("Read_Grid", "Answer");
					hasType = false;
					break;
				case "Brand":
					if (brands.IsNullOrEmpty()) LoadBrands("Read_Grid", "Brand");
					hasType = false;
					break;
				case "Guide":
					if (guides.IsNullOrEmpty()) LoadGuides("Read_Grid", "Guide");
					hasType = true;
					break;
				case "Interview":
					if (interviews.IsNullOrEmpty()) LoadInterviews("Read_Grid", "Interview");
					hasType = false;
					break;
				case "Location":
					if (locations.IsNullOrEmpty())	LoadLocations("Read_Grid", "Location");
					hasType = true;
					break;
				case "Memo":
					if (memos.IsNullOrEmpty()) LoadMemos("Read_Grid", "Memo");
					hasType = true;
					break;
				case "Noun":
					if (nouns.IsNullOrEmpty()) LoadNouns("Read_Grid", "Noun");
					hasType = true;
					break;
				case "NOVA":
					if (novas.IsNullOrEmpty()) LoadNovas("Read_Grid", "NOVA");
					hasType = true;
					break;
				case "Person":
					if (persons.IsNullOrEmpty()) LoadPersons("Read_Grid", "Person");
					hasType = true;
					break;
				case "POD":
					if (pods.IsNullOrEmpty()) LoadPods("Read_Grid", "POD");
					hasType = true;
					break;
				case "Pype":
					if (pypes.IsNullOrEmpty()) LoadPypes("Read_Grid", "Pype");
					hasType = true;
					break;
				case "Question":
					if (questions.IsNullOrEmpty()) LoadQuestions("Read_Grid", "Question");
					hasType = true;
					break;
				case "Registration":
					if (registrations.IsNullOrEmpty()) LoadRegistrations("Read_Grid", "Registration");
					hasType = false;
					break;
				case "Task":
					if (tasks.IsNullOrEmpty()) LoadTasks("Read_Grid", "Task");
					hasType = true;
					break;
				case "URL":
					if (urls.IsNullOrEmpty()) LoadUrls("Read_Grid", "URL");
					hasType = true;
					break;
				case "Verb":
					if (verbs.IsNullOrEmpty()) LoadVerbs("Read_Grid", "Verb");
					hasType = true;
					break;
				case "Work":
					if (works.IsNullOrEmpty()) LoadWorks("Read_Grid", "Work");
					hasType = true;
					break;
				case "-":
					hasType = false;
					break;
			}
			table = _table;
			loading = false;
		}

        private string? DownloadUrl { get; set; }

        private void OnTypeFilterChanged(ChangeEventArgs e)
        {
            typeFilter = e.Value?.ToString() ?? ""; // Update the variable
            Console.WriteLine($"Filter changed to: {typeFilter}");

			if(table == "Noun")
			{
                List<string> filteredNounLabels = nouns
					.Where(noun => string.IsNullOrEmpty(typeFilter) || noun.NounType == typeFilter)
					.Select(noun => noun.NounLabel)
					.ToList();

                // Generate the PDF with filtered data
                byte[] pdfBytes = GeneratePDF(filteredNounLabels);

                // Convert PDF to Base64 and set the download URL
                DownloadUrl = $"data:application/pdf;base64,{Convert.ToBase64String(pdfBytes)}";

                StateHasChanged(); // Refresh UI
            }
        }

        private byte[] GeneratePDF(List<string> nounLabels)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();

                document.Add(new Paragraph("Filtered Nouns"));
                document.Add(new Paragraph("-------------------------------"));

                foreach (var label in nounLabels)
                {
                    document.Add(new Paragraph(label));
                }

                document.Close();
                return ms.ToArray();
            }
        }

        protected override void OnInitialized()
		{
		}
	}
}