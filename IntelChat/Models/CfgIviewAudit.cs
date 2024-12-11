using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgIviewAudit
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public int GuideIdFk { get; set; }

    public short InterviewSeq { get; set; }

    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;
}
