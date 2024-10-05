using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgWorkTask
{
    public int TaskIdFk { get; set; }

    public short WorkLevel { get; set; }

    public string WorkLabel32 { get; set; } = null!;

    public int WorkId { get; set; }

    public string WorkStatus { get; set; } = null!;

    public string WorkDescription { get; set; } = null!;

    public int PodIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string NounLabel { get; set; } = null!;

    public int NounId { get; set; }
}
