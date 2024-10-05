using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Interview
{
    public int GuideId { get; set; }

    public int QuestionId { get; set; }

    public short InterviewSeq { get; set; }

    public string InterviewStatus { get; set; } = null!;
}
