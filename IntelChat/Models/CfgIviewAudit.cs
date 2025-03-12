using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgIviewAudit
{
    public int PersonId { get; set; }

    public int GuideIdFk { get; set; }

    public DateTime MyGuideDtInitial { get; set; }

    public DateTime MyGuideDtFinish { get; set; }

    public string GuidePurpose { get; set; } = null!;
}
