using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string Artist1 { get; set; } = null!;

    public string Life { get; set; } = null!;

    public int Phase { get; set; }

    public int Location { get; set; }

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Finder { get; set; } = null!;

    public string Narative { get; set; } = null!;

    public string ArtistCommon { get; set; } = null!;
}
