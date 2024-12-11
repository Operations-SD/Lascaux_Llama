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

    public DateTime GuideDtOrgin { get; set; }

    public DateTime GuideDtRevision { get; set; }

    public int NovaFk { get; set; }

    public int ProgramFk { get; set; }
}
