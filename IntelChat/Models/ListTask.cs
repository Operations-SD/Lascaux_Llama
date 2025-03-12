using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ListTask
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public short TaskPlatue { get; set; }

    public string TaskDescription { get; set; } = null!;
}
