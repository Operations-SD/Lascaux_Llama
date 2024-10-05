using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeeklyActivity
{
    public int WeekId { get; set; }

    public int PersonIdFk { get; set; }

    public int NounIdFk { get; set; }

    public short Quantity { get; set; }
}
