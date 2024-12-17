using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodTask
{
    public int PodIdFk { get; set; }

    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public short TaskLevel { get; set; }

    public short TaskSeq { get; set; }

    public int NovaIdFk { get; set; }
}
