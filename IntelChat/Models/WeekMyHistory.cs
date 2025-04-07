using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeekMyHistory
{
    public short WeekCalendarWeekId { get; set; }

    public int PersonId { get; set; }

    public int WeekCatMinorId { get; set; }

    public short? WeekWatchElem1 { get; set; }

    public short? WeekWatchElem2 { get; set; }

    public short? WeekWatchElem3 { get; set; }

    public short? WeekWatchElem4 { get; set; }

    public short? WeekWatchElem5 { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual WeekCalendar WeekCalendarWeek { get; set; } = null!;

    public virtual LpMinor WeekCatMinor { get; set; } = null!;
}
