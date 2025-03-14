using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MyGoal
{
    public int PersonIdFk { get; set; }

    public int TaskIdFk { get; set; }

    public string MyGoalType { get; set; } = null!;

    public DateTime MyGoalDtStart { get; set; }

    public DateTime MyGoalDtFinish { get; set; }
}
