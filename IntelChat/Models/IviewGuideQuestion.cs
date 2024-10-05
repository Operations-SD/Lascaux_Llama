using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class IviewGuideQuestion
{
    public short InterviewSeq { get; set; }

    public string GuideLabel { get; set; } = null!;

    public string QuestionText { get; set; } = null!;
}
