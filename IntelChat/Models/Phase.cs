using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Phase
{
    public int PhaseId { get; set; }

    public string Phase1 { get; set; } = null!;

    public string Period { get; set; } = null!;

    public int Location { get; set; }

    public string Status { get; set; } = null!;

    public string Finder { get; set; } = null!;

    public string Narative { get; set; } = null!;
}
