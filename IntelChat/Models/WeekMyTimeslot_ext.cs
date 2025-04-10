using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeekMyTimeslot
{

    // HS added type casting ie (short), (byte)
    public void BuildFromViewslot(ViewSlot v)
    {
        PersonId = v.PersonId;

        WeekCalendarWeekId = (short)v.WeekCalendarWeekId;

        WeekDay = (byte)v.WeekDay;

        WeekTimeslot = (byte)v.WeekTimeslot;

        WeekOffset = (byte)v.WeekOffset;

        WeekPriority = (byte?)v.WeekPriority;

        // HS WeekDuration = v.TimeDuration;

        WeekAlarmOffset = (byte?)v.WeekAlarmOffset;

        WeekStatus = v.WeekStatus;

        WeekActualFactor = v.WeekActualFactor;

        WeekCatMinorId = v.WeekCatMinorId;
    }
}
