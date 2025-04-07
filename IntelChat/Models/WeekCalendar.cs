using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeekCalendar
{
    public short WeekCalendarWeekId { get; set; }

    public DateTime? WeekCalendarDate { get; set; }

    public virtual ICollection<WeekMyHistory> WeekMyHistories { get; } = new List<WeekMyHistory>();

    public virtual ICollection<WeekMyTimeslot> WeekMyTimeslots { get; } = new List<WeekMyTimeslot>();
}
