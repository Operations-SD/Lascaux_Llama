using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class IviewPersonQAnswered
{
    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public string GuidePurpose { get; set; } = null!;

    public string GuideType { get; set; } = null!;

    public short InterviewSeq { get; set; }

    public string QuestionText { get; set; } = null!;

    public int NovaIdFk { get; set; }

    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public short AnswerResponse { get; set; }

    public short AnswerSeverity { get; set; }
}
