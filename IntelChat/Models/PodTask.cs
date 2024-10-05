using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodTask
{
    public int PodId { get; set; }

    public short TaskLevel { get; set; }

    public int TaskId { get; set; }

    public string TaskType { get; set; } = null!;

    public string TaskLabel32 { get; set; } = null!;

    public DateTime TaskStartDate { get; set; }

    public DateTime TaskFinishDate { get; set; }

    public string TaskDescription { get; set; } = null!;

    public DateTime ParentTaskStart { get; set; }
}
