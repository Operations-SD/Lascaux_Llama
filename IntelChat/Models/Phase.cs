using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Phase
{
    public int PhaseId { get; set; }

    public string? Phase1 { get; set; }

    public string? Period { get; set; }

    public int? Location { get; set; }

    public string? Status { get; set; }

    public string? Finder { get; set; }

    public string? Narative { get; set; }
}
