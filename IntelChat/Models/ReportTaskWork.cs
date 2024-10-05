using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ReportTaskWork
{
    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public int WorkId { get; set; }

    public string WorkLabel32 { get; set; } = null!;

    public string WorkType { get; set; } = null!;

    public string WorkDescription { get; set; } = null!;

    public int PodIdFk { get; set; }
}
