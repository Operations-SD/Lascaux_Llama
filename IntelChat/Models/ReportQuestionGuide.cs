using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ReportQuestionGuide
{
    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public string GuidePurpose { get; set; } = null!;

    public string GuideType { get; set; } = null!;

    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;
}
