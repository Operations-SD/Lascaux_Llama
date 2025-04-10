using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeekCalendar
{
    public short WeekCalendarWeekId { get; set; }

    public DateOnly? WeekCalendarDate { get; set; }
}
