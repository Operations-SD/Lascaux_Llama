using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class DeltaTask
{
    public int PodIdFk { get; set; }

    public string TaskType { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskDescription { get; set; } = null!;

    public short TaskPlatue { get; set; }
}
