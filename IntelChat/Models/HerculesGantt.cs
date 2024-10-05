using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class HerculesGantt
{
    public int Id { get; set; }

    public string String { get; set; } = null!;

    public DateTime Sdate { get; set; }

    public DateTime Edate { get; set; }

    public string Progress { get; set; } = null!;

    public int Parentid { get; set; }

    public string Duration { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public DateTime BaselineStartDate { get; set; }

    public DateTime BaselineEndDate { get; set; }

    public string Predecessor { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public int ProjectId { get; set; }

    public string IsExpand { get; set; } = null!;
}
