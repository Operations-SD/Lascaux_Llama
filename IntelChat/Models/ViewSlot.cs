using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ViewSlot
{
    public int PersonId { get; set; }

    public short WeekCalendarWeekId { get; set; }

    public byte WeekDay { get; set; }

    public byte WeekTimeslot { get; set; }

    public byte WeekOffset { get; set; }

    public byte? WeekPriority { get; set; }

    public byte? WeekDuration { get; set; }

    public byte? WeekAlarmOffset { get; set; }

    public string? WeekStatus { get; set; }

    public float? WeekActualFactor { get; set; }

    public string LpMajorLabel { get; set; } = null!;

    public string LpMajorDesc { get; set; } = null!;

    public string LpMinorLabel { get; set; } = null!;

    public string LpMinorDesc { get; set; } = null!;

    public string LpMajorRgb { get; set; } = null!;

    public int WeekCatMinorId { get; set; }
}
