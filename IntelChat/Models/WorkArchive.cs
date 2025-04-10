using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WorkArchive
{
    public int WorkId { get; set; }

    public string WorkLabel { get; set; } = null!;

    public string WorkType { get; set; } = null!;

    public string WorkStatus { get; set; } = null!;

    public string WorkRole { get; set; } = null!;

    public string WorkText { get; set; } = null!;

    public byte WorkIntR { get; set; }

    public byte WorkIntS { get; set; }

    public int QuestionIdFk { get; set; }

    public int TaskIdFk { get; set; }

    public int GuideIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public string WorkTag { get; set; } = null!;
}
