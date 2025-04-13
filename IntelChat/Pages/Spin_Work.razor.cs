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
    public partial class Spin_Work
    {
        [Inject]
        public NotificationService NotificationService { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? pid { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? pod { get; set; }

        private List<Work> entities = new List<Work>();
        private Work? SelectedWork { get; set; }

        private int? selectedTaskId = null;
        private string? selectedWorkType = null;
        private byte? selectedWorkIntS = null;
        private string? tagFilter = null;

        private class TaskItem
        {
            public int Id { get; set; }
            public string Label { get; set; }
        }

        private List<TaskItem> taskItems = new();

        private int _selectedIndex = 0;

        private class WorkTypeItem
        {
            public string Type { get; set; }
            public string Label { get; set; }
        }

        private List<WorkTypeItem> workTypes = new();

        private List<byte> workIntSLevels = new();

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
        private SqlDataReader? Read()
        {
            if (!selectedTaskId.HasValue) return null;

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PROC_action", "Read"),
                new SqlParameter("@Task_ID_FK", selectedTaskId.Value)
            };

            if (!string.IsNullOrEmpty(selectedWorkType))
                parameters.Add(new SqlParameter("@Work_type", selectedWorkType));

            if (selectedWorkIntS.HasValue)
                parameters.Add(new SqlParameter("@Work_int_S", selectedWorkIntS.Value));

            if (!string.IsNullOrEmpty(tagFilter))
                parameters.Add(new SqlParameter("@Work_tag", tagFilter));

            return ExecuteStoredProcedure("dbo.[Spin_Work]", parameters, true);
        }

        /// <summary>Load entities from the database into a list </summary>
        /// <param name="status">Status of the entities that will be loaded</param>
        private void LoadReadResults()
        {
            if (!selectedTaskId.HasValue)
            {
                entities.Clear();
                SelectedWork = null;
                NotificationService.Notify("Please select a Task before applying filters.", NotificationType.Error);
                return;
            }

            using var reader = Read();
            if (reader == null) return;

            entities.Clear();
            while (reader.Read())
            {
                entities.Add(new Work
                {
                    WorkId = reader.GetInt32(0),
                    WorkLabel = reader.GetString(1),
                    WorkType = reader.GetString(2),
                    WorkStatus = reader.GetString(3),
                    WorkDescription = reader.GetString(4),
                    GuideIdFk = reader.GetInt32(5),
                    QuestionIdFk = reader.GetInt32(6),
                    WorkIntR = reader.GetByte(7),
                    WorkIntS = reader.GetByte(8),
                    PersonIdFk = reader.GetInt32(9),
                    WorkSkill = reader.GetString(10),
                    TaskIdFk = reader.GetInt32(11),
                    NovaIdFk = reader.GetInt32(12),
                    WorkTag = reader.GetString(13),
                    WorkTimeUnits = reader.GetInt16(14),
                    WorkSeq = reader.GetByte(15)
                });
            }

            reader.Close();

            if (entities.Any())
            {
                _selectedIndex = 0;
                SetSelectedWork();
            }
            else
            {
                SelectedWork = null;
                NotificationService.Notify("No Work entries found for the current filters.", NotificationType.Info);
            }
        }

        // Populate the Task dropdown with ID-label pairs based on existing Work entries
        private void LoadTaskIds()
        {
            List<SqlParameter> parameters = new() { new SqlParameter("@PROC_action", "GetTaskIDs") };
            using var reader = ExecuteStoredProcedure("dbo.[Spin_Work]", parameters, true);
            if (reader == null) return;

            taskItems.Clear();
            while (reader.Read())
            {
                taskItems.Add(new TaskItem
                {
                    Id = reader.GetInt32(0),
                    Label = reader.GetString(1)
                });
            }

            reader.Close();
        }

        // Populate the Work Type dropdown with unique types and display labels
        private void LoadWorkTypes()
        {
            List<SqlParameter> parameters = new() { new SqlParameter("@PROC_action", "GetWorkTypes") };
            using var reader = ExecuteStoredProcedure("dbo.[Spin_Work]", parameters, true);
            if (reader == null) return;

            workTypes.Clear();
            while (reader.Read())
            {
                workTypes.Add(new WorkTypeItem
                {
                    Type = reader.GetString(0),
                    Label = reader.GetString(1)
                });
            }

            reader.Close();
        }

        // Populate the Work Intensity dropdown with distinct Work_int_S values
        private void LoadWorkIntSLevels()
        {
            List<SqlParameter> parameters = new() { new SqlParameter("@PROC_action", "GetWorkIntSLevels") };
            using var reader = ExecuteStoredProcedure("dbo.[Spin_Work]", parameters, true);
            if (reader == null) return;

            workIntSLevels.Clear();
            while (reader.Read())
                workIntSLevels.Add(reader.GetByte(0));

            reader.Close();
        }

        // Set the current SelectedWork based on the selected index
        private void SetSelectedWork()
        {
            if (entities.Count > 0 && _selectedIndex >= 0 && _selectedIndex < entities.Count)
            {
                SelectedWork = entities[_selectedIndex];
            }
        }

        // Move to the next item in the result set
        private void GoToNextWork()
        {
            if (_selectedIndex < entities.Count - 1)
            {
                _selectedIndex++;
                SetSelectedWork();
            }
        }

        // Move to the previous item in the result set
        private void GoToPreviousWork()
        {
            if (_selectedIndex > 0)
            {
                _selectedIndex--;
                SetSelectedWork();
            }
        }

        // Update Task_ID filter and reload results
        private void OnTaskChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int taskId))
                selectedTaskId = taskId;

            LoadReadResults();
        }

        // Update Work Type filter (requires Task selection first)
        private void OnWorkTypeChanged(ChangeEventArgs e)
        {
            if (!selectedTaskId.HasValue)
            {
                NotificationService.Notify("Please select a Task before choosing a Work Type.", NotificationType.Error);
                return;
            }

            selectedWorkType = e.Value?.ToString();
            if (selectedWorkType == "All") selectedWorkType = null;

            LoadReadResults();
        }

        // Update Work Intensity filter (requires Task selection first)
        private void OnWorkIntSChanged(ChangeEventArgs e)
        {
            if (!selectedTaskId.HasValue)
            {
                NotificationService.Notify("Please select a Task before choosing a Work Int S level.", NotificationType.Error);
                return;
            }

            if (byte.TryParse(e.Value?.ToString(), out byte val))
                selectedWorkIntS = val;
            else
                selectedWorkIntS = null;

            LoadReadResults();
        }

        // Update tag filter input and refresh results
        private async ThreadingTask OnTagFilterChanged(ChangeEventArgs e)
        {
            tagFilter = e.Value?.ToString();
            await ThreadingTask.Delay(300); // debounce
            LoadReadResults();
        }

        // Reset all filters and clear displayed result
        private void ResetFilters()
        {
            selectedTaskId = null;
            selectedWorkType = null;
            selectedWorkIntS = null;
            tagFilter = null;

            entities.Clear();
            SelectedWork = null;

            NotificationService.Notify("All filters have been reset.", NotificationType.Info);
        }

        // Load all dropdowns and result set when component is first rendered
        protected override void OnInitialized()
        {
            LoadTaskIds();
            LoadWorkTypes();
            LoadWorkIntSLevels();
            LoadReadResults();
        }

        // Getter/setter for the selected item index in the results list
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    SetSelectedWork();
                }
            }
        }
    }
}