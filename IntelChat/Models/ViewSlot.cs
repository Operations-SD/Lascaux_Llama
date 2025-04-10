using System;
using System.Collections.Generic;

namespace IntelChat.Models;

/* HS 
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
*/

// HS 
public class ViewSlot
{
    public int PersonId { get; set; }
    public int WeekCalendarWeekId { get; set; }
    public int WeekDay { get; set; }
    public int WeekTimeslot { get; set; }
    public int WeekOffset { get; set; }
    public int WeekPriority { get; set; }
    public int WeekAlarmOffset { get; set; }
    public string WeekStatus { get; set; }
    public int WeekActualFactor { get; set; }
    public int WeekCatMinorId { get; set; }

    public string LpMajorLabel { get; set; }
    public string LpMajorDesc { get; set; }
    public string LpMajorRgb { get; set; }

    public string LpMinorLabel { get; set; }
    public string LpMinorDesc { get; set; }
}