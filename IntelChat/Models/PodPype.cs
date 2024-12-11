using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodPype
{
    public int? PodId { get; set; }

    public string? PodLabel { get; set; }

    public string? PypeLabel { get; set; }

    public string? PodType { get; set; }

    public string? PypeType { get; set; }

    public string? NounLabel { get; set; }
}
