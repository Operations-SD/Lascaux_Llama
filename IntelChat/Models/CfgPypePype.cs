using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgPypePype
{
    public string PypeId { get; set; } = null!;

    public string PypeLabel { get; set; } = null!;

    public string PypeStatus { get; set; } = null!;

    public string PypeLink { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string Expr1 { get; set; } = null!;

    public string Expr2 { get; set; } = null!;

    public string Expr3 { get; set; } = null!;

    public int PodIdFk { get; set; }
}
