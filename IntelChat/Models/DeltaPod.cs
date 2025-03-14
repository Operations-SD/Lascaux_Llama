using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class DeltaPod
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string PodType { get; set; } = null!;

    public string PodStatus { get; set; } = null!;

    public string PodPypeDdChan { get; set; } = null!;

    public string PodImage { get; set; } = null!;

    public int PersonIdFk { get; set; }

    public int LocationIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public int Expr1 { get; set; }
}
