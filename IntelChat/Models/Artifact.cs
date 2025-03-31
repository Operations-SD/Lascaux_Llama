using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Artifact
{
    public int ArtifactId { get; set; }

    public string Artifact1 { get; set; } = null!;

    public string ArtifactImage { get; set; } = null!;

    public int Artist { get; set; }

    public int Phase { get; set; }

    public int Location { get; set; }

    public int Composition { get; set; }

    public string Status { get; set; } = null!;

    public string? Finder { get; set; }

    public string? Narative { get; set; }

    public DateTime? HistoricalDate { get; set; }

    public int? Symbolic { get; set; }
}
