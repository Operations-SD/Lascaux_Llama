using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MyWeekHistory
{
    public int IdPersonFk { get; set; }

    public short TimeCalendarWeekId { get; set; }

    public int NovaIdFk { get; set; }

    public short TimeObjectSum { get; set; }
}
