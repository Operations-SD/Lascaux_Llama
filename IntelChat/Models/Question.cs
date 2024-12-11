using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;

    public string QuestionStatus { get; set; } = null!;

    public int NovaIdFk { get; set; }
}
