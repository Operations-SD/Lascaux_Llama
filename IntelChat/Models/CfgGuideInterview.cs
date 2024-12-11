using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgGuideInterview
{
    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public int QuestionId { get; set; }

    public short InterviewSeq { get; set; }

    public string InterviewStatus { get; set; } = null!;

    public string QuestionText { get; set; } = null!;
}
