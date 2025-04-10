using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WkMyTimeslot
{
    public int IdPerson { get; set; }

    public short TimeCalendarWeekId { get; set; }

    public byte TimeDay { get; set; }

    public byte TimeTimeslot { get; set; }

    public int TimeCatMinorId { get; set; }

    public byte TimeOffset { get; set; }

    public byte? TimePriority { get; set; }

    public byte? TimeDuration { get; set; }

    public byte? TimeAlarmOffset { get; set; }

    public string? TimeStatus { get; set; }

    public float? TimeActualFactor { get; set; }

    public string? TimeMyNote { get; set; }
}
