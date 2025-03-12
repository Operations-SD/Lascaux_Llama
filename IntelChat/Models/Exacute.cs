using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Exacute
{
    public string CommandIdFk { get; set; } = null!;

    public DateTime ExacuteDt { get; set; }

    public int ChannelIdFk { get; set; }

    public int ChannelFkLog { get; set; }

    public int ExacuteFrom { get; set; }

    public int ExacuteTo { get; set; }

    public string ExacuteToType { get; set; } = null!;

    public string ExacuteStatus { get; set; } = null!;

    public string ExacuteText { get; set; } = null!;

    public byte ExacutePriority { get; set; }

    public int GuideIdFk { get; set; }

    public int QuestionIdFk { get; set; }

    public byte ExacuteR { get; set; }

    public byte ExacuteS { get; set; }
}
