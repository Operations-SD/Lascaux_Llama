using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgTaskL4Noun
{
    public int Pod { get; set; }

    public short Lvl { get; set; }

    public string Type { get; set; } = null!;

    public int TaskId { get; set; }

    public string Label { get; set; } = null!;

    public DateTime TaskStartDate { get; set; }

    public DateTime TaskFinishDate { get; set; }

    public string TaskDescription { get; set; } = null!;

    public DateTime ParStart { get; set; }

    public DateTime ParFinish { get; set; }

    public int ParId { get; set; }

    public int? Nova { get; set; }

    public int? NounId { get; set; }

    public string? Object { get; set; }
}
