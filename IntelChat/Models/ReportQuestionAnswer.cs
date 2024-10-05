using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ReportQuestionAnswer
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public string PersonLabel { get; set; } = null!;

    public string QuestionText { get; set; } = null!;

    public short AnswerResponse { get; set; }

    public short AnswerSeverity { get; set; }
}
