using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Hercule
{
    public short TaskLevel { get; set; }

    public string TaskType { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskDescription { get; set; } = null!;

    public int TaskPrevious { get; set; }

    public int PodIdFk { get; set; }

    public short TaskDuration { get; set; }

    public int TaskParent { get; set; }
}
