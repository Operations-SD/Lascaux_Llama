using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ReportArtifact
{
    public int ArtifactId { get; set; }

    public string Artifact { get; set; } = null!;

    public int ArtistId { get; set; }

    public string Artist { get; set; } = null!;

    public int PhaseId { get; set; }

    public string Phase { get; set; } = null!;

    public int LocationId { get; set; }

    public string LocationLabel16 { get; set; } = null!;

    public int CompositionId { get; set; }

    public string Composition { get; set; } = null!;
}
