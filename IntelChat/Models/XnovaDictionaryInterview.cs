using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class XnovaDictionaryInterview
{
    public string About { get; set; } = null!;

    public int P { get; set; }

    public int G { get; set; }

    public int Q { get; set; }

    public string QuestionText { get; set; } = null!;

    public int? N { get; set; }

    public string? NovaDescription { get; set; }

    public int? S { get; set; }

    public string? Subject { get; set; }

    public string? SubjectDescription { get; set; }

    public string? SubjectUrl { get; set; }

    public int? V { get; set; }

    public string? Verb { get; set; }

    public string? VerbDescription { get; set; }

    public string? VerbUrl { get; set; }

    public int? O { get; set; }

    public string? Object { get; set; }

    public string? ObjectDescription { get; set; }

    public string? ObjectUrl { get; set; }

    public int? Pid { get; set; }
}
