using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodTaskWork
{
    public int PodId { get; set; }

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public int WorkId { get; set; }

    public string WorkType { get; set; } = null!;

    public string WorkDescription { get; set; } = null!;

    public int? Expr1 { get; set; }

    public string? NovaType { get; set; }

    public string? NovaDescription { get; set; }

    public string? Subject { get; set; }

    public string? Verb { get; set; }

    public string? Object { get; set; }
}
