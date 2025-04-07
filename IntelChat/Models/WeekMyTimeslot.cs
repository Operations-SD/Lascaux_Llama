using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeekMyTimeslot
{
    public int PersonId { get; set; }

    public short WeekCalendarWeekId { get; set; }

    public byte WeekDay { get; set; }

    public byte WeekTimeslot { get; set; }

    public byte WeekOffset { get; set; }

    public int WeekCatMinorId { get; set; }

    public byte? WeekPriority { get; set; }

    public byte? WeekDuration { get; set; }

    public byte? WeekAlarmOffset { get; set; }

    public string? WeekStatus { get; set; }

    public float? WeekActualFactor { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual WeekCalendar WeekCalendarWeek { get; set; } = null!;

    public virtual LpMinor WeekCatMinor { get; set; } = null!;
}
