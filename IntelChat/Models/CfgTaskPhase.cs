using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgTaskPhase
{
    public int TaskId { get; set; }

    public string TaskType { get; set; } = null!;

    public short TaskPlatue { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public DateTime TaskStartDate { get; set; }
}
