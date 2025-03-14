using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ProgramSpaQ
{
    public int ProgramId { get; set; }

    public string ProgramLabel { get; set; } = null!;

    public string ProgramDesc { get; set; } = null!;

    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public int QuestionId { get; set; }

    public short InterviewSeq { get; set; }

    public int Expr1 { get; set; }

    public string QuestionText { get; set; } = null!;
}
