using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ViewSlot
{
    public int IdPersonFk { get; set; }

    public short TimeCalendarWeekId { get; set; }

    public byte TimeDay { get; set; }

    public byte TimeTimeslot { get; set; }

    public byte TimeOffset { get; set; }

    public byte TimePriority { get; set; }

    public byte TimeAlarmOffset { get; set; }

    public string TimeStatus { get; set; } = null!;

    public double TimeObjectFactor { get; set; }

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string NovaLabel { get; set; } = null!;

    public string NovaDescription { get; set; } = null!;

    public string PodRgb { get; set; } = null!;

    public int NovaIdFk { get; set; }

    public int Expr1 { get; set; }
}
