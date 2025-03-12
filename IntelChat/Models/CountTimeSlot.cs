using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CountTimeSlot
{
    public int IdPersonFk { get; set; }

    public short TimeCalendarWeekId { get; set; }

    public string NounLabel { get; set; } = null!;

    public double? Expr1 { get; set; }
}
