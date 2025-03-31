using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MyGuide
{
    public int PersonIdFk { get; set; }

    public int GuideIdFk { get; set; }

    public DateTime MyGuideDtInitial { get; set; }

    public DateTime MyGuideDtFinish { get; set; }

    public DateTime MyGuideDtEvaluated { get; set; }

    public byte MyGuideStars { get; set; }

    public string MyGuide1 { get; set; } = null!;
}
