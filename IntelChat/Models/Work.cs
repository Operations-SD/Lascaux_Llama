﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Work
{
    public int WorkId { get; set; }

    public string WorkLabel { get; set; } = null!;

    public string WorkType { get; set; } = null!;

    public string WorkStatus { get; set; } = null!;

    public string WorkDescription { get; set; } = null!;

    public int GuideIdFk { get; set; }

    public int QuestionIdFk { get; set; }

    public byte WorkIntR { get; set; }

    public byte WorkIntS { get; set; }

    public int PersonIdFk { get; set; }

    public string WorkSkill { get; set; } = null!;

    public int TaskIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public string WorkTag { get; set; } = null!;

    public short WorkTimeUnits { get; set; }

    public byte WorkSeq { get; set; }
}
