using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class TaskWorkGantt
{
    public string? TaskType { get; set; }

    public int? TaskId { get; set; }

    public short? TaskLevel { get; set; }

    public string? TaskDescription { get; set; }

    public DateTime? TaskStartDate { get; set; }

    public string? WorkDescription { get; set; }

    public short? WorkStart { get; set; }

    public int? TaskParent { get; set; }
}
