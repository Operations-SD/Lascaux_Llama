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
	public partial class Page_URL_Admn
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
		private List<Url> entities = new List<Url>();
		private Dictionary<String, Url> entity = new Dictionary<String, Url>();
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

		/// <summary>Reads entities from the database using a stored procedure</summary>
		/// <param name="status">Status of the entities that will be read</param>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? Read(string status = "*")
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@PROC_filter", "****"),
				new SqlParameter("@URL_status", status)
				//pod?
			};
			return ExecuteStoredProcedure("dbo.[CRUD_URL]", parameters, true);
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
				entities.Add(new Url
				{
					UrlId = reader.GetInt32(0),
					UrlLabel = reader.GetString(1),
					UrlDescription = reader.GetString(2),
					UrlType = reader.GetString(3),
					UrlStatus = reader.GetString(4),
					UrlCloud = reader.GetString(5),
					UrlAdvanceLevel = reader.GetByte(6),
					NovaIdFk = reader.GetInt32(7),
					UrlTag = reader.GetString(8),
					UrlChain = reader.GetInt32(9),
					UrlStars = reader.GetByte(10),

				});
			}
			reader.Close();
		}

		/// <summary>Fill fields in the change tab based on entity selection</summary>
		private void AutoFill(int id, String type)
		{
			var target = entities.Find(e => e.UrlId == id);
			if (target != null) entity[type] = target;
		}


		/// <summary>Reads pypes from the database using a stored procedure</summary>
		/// <returns>Reader for the entities that were read from the database</returns>
		private SqlDataReader? ReadPype()
		{
			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@PROC_Input_Filter", "URLS")
			};
			return ExecuteStoredProcedure("dbo.[sp_Pype_Type_NoPOD]", parameters, true);
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

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    SetSelectedUrl();
                }
            }
        }

        private Url? SelectedUrl { get; set; }

        private void SetSelectedUrl()
        {
            if (entities.Count > 0 && _selectedIndex >= 0 && _selectedIndex < entities.Count)
            {
                SelectedUrl = entities[_selectedIndex];
            }
        }

        private void GoToNextUrl()
        {
            if (_selectedIndex < entities.Count - 1)
            {
                SelectedIndex++;
            }
        }

        private void GoToPreviousUrl()
        {
            if (_selectedIndex > 0)
            {
                SelectedIndex--;
            }
        }

        protected override void OnInitialized()
        {
            LoadReadResults();

            if (entities.Any())
            {
                _selectedIndex = 0;
                SetSelectedUrl();
            }
        }

        /// Determine if a URL is an image
        private bool IsImage(string url)
        {
            return url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                   url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                   url.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                   url.EndsWith(".gif", StringComparison.OrdinalIgnoreCase);
        }

        /// Determine if a URL is a YouTube link
        private bool IsYouTube(string url)
        {
            return url.Contains("youtube.com/watch") || url.Contains("youtu.be/");
        }

        /// Convert YouTube URL to embed format
        private string GetYouTubeEmbedUrl(string url)
        {
            if (url.Contains("youtu.be/"))
            {
                return "https://www.youtube.com/embed/" + url.Split('/').Last();
            }
            else if (url.Contains("watch?v="))
            {
                return "https://www.youtube.com/embed/" + url.Split("watch?v=").Last().Split('&').First();
            }
            return url;
        }

        /// Determine if a URL is a PDF
        private bool IsPDF(string url)
        {
            return url.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
        }
    }
}