using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LoadGuideQ
{
    public int GuideId { get; set; }

    public string GuideLabel { get; set; } = null!;

    public short Seq { get; set; }

    public int QId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int? IvewProg { get; set; }

    public int? GuideIdFk { get; set; }

    public int GProgram { get; set; }
}
