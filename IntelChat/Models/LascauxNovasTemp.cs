using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LascauxNovasTemp
{
    public string From { get; set; } = null!;

    public int N { get; set; }

    public string Ntype { get; set; } = null!;

    public string Ndesc { get; set; } = null!;

    public int S { get; set; }

    public string Subject { get; set; } = null!;

    public int V { get; set; }

    public string Verb { get; set; } = null!;

    public int O { get; set; }

    public string Object { get; set; } = null!;

    public string Sdesc { get; set; } = null!;

    public string Vdesc { get; set; } = null!;

    public string Odesc { get; set; } = null!;

    public int P { get; set; }

    public string? Pdesc { get; set; }

    public int? X { get; set; }

    public string? Xtype { get; set; }

    public string? Xdesc { get; set; }
}
