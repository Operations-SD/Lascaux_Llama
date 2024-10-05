using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgTaskParmPt
{
    public int PodIdFk { get; set; }

    public short TaskLevel { get; set; }

    public string TaskType { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public DateTime TaskStartDate { get; set; }

    public DateTime TaskFinishDate { get; set; }

    public string TaskDescription { get; set; } = null!;

    public DateTime ParStart { get; set; }

    public DateTime ParFinish { get; set; }

    public int Parameter { get; set; }

    public int NovaIdFk { get; set; }

    public string NovaDescription { get; set; } = null!;

    public string Object { get; set; } = null!;
}
