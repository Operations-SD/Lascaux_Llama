using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Composition
{
    public int CompositionId { get; set; }

    public string Composition1 { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Finder { get; set; } = null!;

    public string Narative { get; set; } = null!;
}
