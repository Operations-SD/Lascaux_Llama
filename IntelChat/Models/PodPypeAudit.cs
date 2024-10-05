using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodPypeAudit
{
    public int? PodIdFk { get; set; }

    public string? PypeId { get; set; }

    public string? PypeType { get; set; }

    public string? PypeLabel { get; set; }

    public string Drop { get; set; } = null!;

    public string Drop16 { get; set; } = null!;

    public string? PypeStatus { get; set; }

    public string? PypeLink { get; set; }
}
