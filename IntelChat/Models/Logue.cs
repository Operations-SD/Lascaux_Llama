using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Logue
{
    public int Pod { get; set; }

    public int Pid { get; set; }

    public DateTime DateTime { get; set; }

    public string Command { get; set; } = null!;
}
