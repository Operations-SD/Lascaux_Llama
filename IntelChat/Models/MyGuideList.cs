using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MyGuideList
{
    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public string GuidePurpose { get; set; } = null!;

    public string GuideType { get; set; } = null!;

    public string GuideStatus { get; set; } = null!;

    public int GuideImage { get; set; }

    public DateTime GuideDtOrgin { get; set; }

    public DateTime GuideDtRevision { get; set; }

    public string GuideTags { get; set; } = null!;

    public byte GuideEligible { get; set; }

    public int NovaIdFk { get; set; }

    public int PodIdFk { get; set; }

    public int BrainIdFk { get; set; }

    public int UrlIdFk { get; set; }

    public int ProgramFk { get; set; }
}
