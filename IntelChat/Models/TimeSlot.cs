using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class TimeSlot
{
    public int IdPersonFk { get; set; }

    public short TimeCalendarWeekId { get; set; }

    public byte TimeDay { get; set; }

    public byte TimeTimeslot { get; set; }

    public byte TimeOffset { get; set; }

    public byte TimePriority { get; set; }

    public byte TimeObject { get; set; }

    public byte TimeAlarmOffset { get; set; }

    public string TimeStatus { get; set; } = null!;

    public double TimeObjectFactor { get; set; }

    public int MemoIdFk { get; set; }

    public int PodIdFk { get; set; }

    public int NovaIdFk { get; set; }
}
