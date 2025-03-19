using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Task = IntelChat.Models.Task;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Spin_URL
	{
        [Inject]
        public NotificationService NotificationService { get; set; }

        [Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }

		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }

		private List<Url> entities = new List<Url>();
		//private Dictionary<String, Url> entity = new Dictionary<String, Url>();
        //private Dictionary<String, String> filter = new Dictionary<String, String>();

        private Url? SelectedUrl { get; set; }

        //Filter for priority (Advanced Level)
        private byte? priorityFilter { get; set; } = null;
        private List<byte> priorityLevels = new List<byte>(); // Stores unique priority levels

        private List<string> urlTypes = new List<string>(); // Stores unique URL types
        private string? selectedUrlType = null; // Stores selected URL type
        private string? tagFilter { get; set; } = null; //used to filter by tag
        private bool isChainedView = false; // Track if user is viewing a chained URL
        private int rootIndexBeforeChain = -1; // Stores the index before chaining
        private int rootUrlId = -1;

        private List<Url> originalUrls = new List<Url>(); //Store original unchained list
        



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
        private SqlDataReader? Read(string status = "*", byte? priority = null, string? urlType = null, string? tag = null, int? chainedId = null)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PROC_action", "Read"),
                new SqlParameter("@URL_status", status)
            };

            if (priority.HasValue)
                parameters.Add(new SqlParameter("@URL_advance_level", priority.Value)); // Apply priority filter

            if (!string.IsNullOrEmpty(urlType))
                parameters.Add(new SqlParameter("@URL_type", urlType)); // Apply URL Type filter

            if (!string.IsNullOrEmpty(tag))
                parameters.Add(new SqlParameter("@URL_tag", tag)); // Apply tag filter

            if (chainedId.HasValue)
                parameters.Add(new SqlParameter("@URL_ID", chainedId.Value)); // Fetch a specific chained URL

            return ExecuteStoredProcedure("dbo.[Spin_URL]", parameters, true);
        }

        /// <summary>Load entities from the database into a list </summary>
        /// <param name="status">Status of the entities that will be loaded</param>
        private void LoadReadResults( bool isChained = false)
        {
            // If viewing a chained URL, store the original index
            if (isChained && SelectedUrl != null)
            {
                rootIndexBeforeChain = entities.FindIndex(e => e.UrlId == SelectedUrl.UrlId);
            }

            using var reader = isChained && SelectedUrl != null && SelectedUrl.UrlChain != 0
                ? Read(chainedId: SelectedUrl.UrlChain)  // Fetch only the chained URL
                : Read("*", priorityFilter, selectedUrlType, tagFilter); // Fetch filtered list
            
            //using var reader = Read("*", priorityFilter, selectedUrlType, tagFilter);
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

            // Ensure UI updates
            if (entities.Any())
            {
                _selectedIndex = 0; // Set first entry as default
                SetSelectedUrl();
            }

            else
            {
                NotificationService.Notify("URL doesn't exist for current filters.", NotificationType.Error);
            }

            // If viewing a chained URL, update the entities list with only the chained URL
            if (isChained && entities.Any())
            {
                SelectedUrl = entities.First();
                isChainedView = true;
                NotificationService.Notify($"Now viewing chained URL: {SelectedUrl.UrlId}.", NotificationType.Info);
            }
            else
            {
                isChainedView = false;  // Ensure navigation resets properly
            }

            //InvokeAsync(StateHasChanged); // Ensure UI refresh
        }

        /// <summary>Reads distinct priority levels from the database</summary>
        private void LoadPriorityLevels()
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PROC_action", "GetPriorities")
            };

            using var reader = ExecuteStoredProcedure("dbo.[Spin_URL]", parameters, true);
            if (reader == null) return;

            priorityLevels.Clear(); // Clear existing values before populating

            while (reader.Read())
            {
                if (!reader.IsDBNull(0)) // Ensure we don't add NULL values
                {
                    priorityLevels.Add(reader.GetByte(0)); // Read and store priority levels
                }
            }
            reader.Close();

            //InvokeAsync(StateHasChanged); // Force UI refresh after loading
        }

        /// <summary>Reads distinct URL types from the database</summary>
        private void LoadUrlTypes()
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PROC_action", "GetUrlTypes")
            };

            using var reader = ExecuteStoredProcedure("dbo.[Spin_URL]", parameters, true);
            if (reader == null) return;

            urlTypes.Clear();
            while (reader.Read())
            {
                urlTypes.Add(reader.GetString(0)); // Get distinct URL types
            }
            reader.Close();
        }

        /// <summary>Handles changes in the priority filter dropdown</summary>
        private void OnPriorityFilterChanged(ChangeEventArgs e)
        {
            if (byte.TryParse(e.Value?.ToString(), out byte selectedPriority))
                priorityFilter = selectedPriority;
            else
                priorityFilter = null;

            
            LoadReadResults(); // Reload data with filters
        }
                
        /// <summary>Handles changes in the URL Type filter dropdown</summary>
        private void OnUrlTypeFilterChanged(ChangeEventArgs e)
        {
            selectedUrlType = e.Value?.ToString();
            if (selectedUrlType == "All") selectedUrlType = null; // Reset if "All" is selected

            
            LoadReadResults(); // Reload data with filters
        }

        private void OnTagFilterChanged(ChangeEventArgs e)
        {
            tagFilter = e.Value?.ToString();

            _ = ApplyFilters();
            //LoadReadResults(); // Reload data with the new tag filter
        }

        private async ThreadingTask ApplyFilters()
        {
            await ThreadingTask.Delay(300);
            LoadReadResults();
        }

        private void ResetFilters()
        {
            priorityFilter = null;     // Reset priority filter
            selectedUrlType = null;    // Reset URL type filter
            tagFilter = null;          // Reset tag filter
            isChainedView = false;     // Exit chained view if active
            rootIndexBeforeChain = -1; // Reset index tracking for chaining

            LoadReadResults(); // Reload results with cleared filters
            NotificationService.Notify("Filters have been reset.", NotificationType.Success);
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

        private void SetSelectedUrl()
        {
            if (entities.Count > 0 && _selectedIndex >= 0 && _selectedIndex < entities.Count)
            {
                SelectedUrl = entities[_selectedIndex];
            }
        }

        private void GoToNextUrl()
        {
            if (isChainedView)
            {
                // Reload the filtered list
                LoadReadResults();

                int restoredIndex = entities.FindIndex(e => e.UrlId == rootUrlId);

                // Move forward from the original index (before chaining)
                if (restoredIndex >= 0 && restoredIndex < entities.Count - 1)
                {
                    _selectedIndex = restoredIndex + 1; // Move to the next item
                }
                else
                {
                    _selectedIndex = 0; // Fallback to first item
                }

                SelectedUrl = entities[_selectedIndex];
                isChainedView = false;
                rootIndexBeforeChain = -1;
                rootUrlId = -1;
                NotificationService.Notify("Returning to filtered list.", NotificationType.Info);
            }
            else if (_selectedIndex < entities.Count - 1)
            {
                _selectedIndex++;
                SelectedUrl = entities[_selectedIndex];
            }

            //InvokeAsync(StateHasChanged);
        }

        private void GoToPreviousUrl()
        {
            if (isChainedView)
            {
                // Reload the filtered list
                LoadReadResults();

                // Find the index of the originally selected URL in the restored list
                int restoredIndex = entities.FindIndex(e => e.UrlId == SelectedUrl?.UrlId);

                // Move backward from the original index (before chaining)
                if (rootIndexBeforeChain > 0) // Ensure there's a previous item
                {
                    _selectedIndex = rootIndexBeforeChain - 1; // Move to the previous item
                }
                else
                {
                    _selectedIndex = 0; // If at the beginning, stay there
                }

                SelectedUrl = entities[_selectedIndex];
                isChainedView = false;
                NotificationService.Notify("Returning to filtered list.", NotificationType.Info);
            }
            else if (_selectedIndex > 0)
            {
                _selectedIndex--;
                SelectedUrl = entities[_selectedIndex];
            }

            //InvokeAsync(StateHasChanged);
        }

        private void GoToChainUrl()
        {
            if (SelectedUrl != null && SelectedUrl.UrlChain != 0)
            {
                if (rootIndexBeforeChain == -1)
                {
                    rootIndexBeforeChain = entities.FindIndex(e => e.UrlId == SelectedUrl.UrlId);
                    rootUrlId = SelectedUrl.UrlId; // Track the actual ID as well
                }
                LoadReadResults(isChained: true);
            }
        }

        protected override void OnInitialized()
        {
            LoadPriorityLevels(); // Fetch distinct priority levels
            LoadUrlTypes(); // Fetch distinct URL types
            LoadReadResults(); // Load URLs with filters           
        }

        /// Categorize URL type dynamically
        private string GetUrlCategory(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "Unknown";

            // File extensions mapping
            var fileExtension = Path.GetExtension(url).ToLower();
            var imageExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg" };
            var documentExtensions = new HashSet<string> { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
            var videoExtensions = new HashSet<string> { ".mp4", ".webm", ".ogg" };
            var audioExtensions = new HashSet<string> { ".mp3", ".wav", ".ogg" };

            if (imageExtensions.Contains(fileExtension)) return "Image";
            if (documentExtensions.Contains(fileExtension)) return "Document";
            if (videoExtensions.Contains(fileExtension)) return "Video File";
            if (audioExtensions.Contains(fileExtension)) return "Audio File";

            // Special cases for platforms
            if (url.Contains("youtube.com/watch") || url.Contains("youtu.be/")) return "YouTube Video";

            // Default: Assume it's a webpage
            return "Webpage";
        }

        private string GetEmbeddedUrl(string url)
        {
            string category = GetUrlCategory(url);

            return category switch
            {
                "YouTube Video" => GetYouTubeEmbedUrl(url),
                "Video File" => url, // Directly use <video> for embedding
                "Audio File" => url, // Directly use <audio> for embedding
                "Image" => url, // Directly use <img>
                "Document" => url, // PDFs can be embedded
                "Webpage" => url, // External websites open in an iframe
                _ => url // Default: Open as a standard link
            };
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
    }
}