using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeekMyTimeslot
{
    public void BuildFromViewslot(ViewSlot v)
    {
        PersonId = v.PersonId;

        WeekCalendarWeekId = v.WeekCalendarWeekId;

        WeekDay = v.WeekDay;

        WeekTimeslot = v.WeekTimeslot;

        WeekOffset = v.WeekOffset;

        WeekPriority = v.WeekPriority;

        WeekDuration = v.WeekDuration;

        WeekAlarmOffset = v.WeekAlarmOffset;

        WeekStatus = v.WeekStatus;

        WeekActualFactor = v.WeekActualFactor;

        WeekCatMinorId = v.WeekCatMinorId;
    }
}
