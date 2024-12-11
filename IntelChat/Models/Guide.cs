using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Guide
{
    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public string GuidePurpose { get; set; } = null!;

    public string GuideType { get; set; } = null!;

    public string GuideStatus { get; set; } = null!;

    public int GuideImage { get; set; }

    public DateTime GuideDateOrgin { get; set; }

    public DateTime GuideDateRevision { get; set; }

    public string GuideTag { get; set; } = null!;

    public int NovaIdFk { get; set; }

    public int PodIdFk { get; set; }

    public int BrainIdFk { get; set; }

    public int UrlIdFk { get; set; }

    public byte GuideEligible { get; set; }
}
