using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Answer
{
    public int PersonId { get; set; }

    public int QuestionId { get; set; }

    public short AnswerResponse { get; set; }

    public short AnswerSeverity { get; set; }

    public DateTime AnswerDtOrgin { get; set; }

    public DateTime AnswerDtRevision { get; set; }
}
