using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgPypeStem
{
    public int PodIdFk { get; set; }

    public string Pype { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string ParentPype { get; set; } = null!;

    public string? L1Id { get; set; }

    public string? L1Type { get; set; }

    public string? PypeLabel { get; set; }

    public string? PypeDesc { get; set; }

    public int? Expr1 { get; set; }
}
