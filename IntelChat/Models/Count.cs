using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Count
{
    public int Person { get; set; }

    public short Week { get; set; }

    public string Object { get; set; } = null!;

    public double? Units { get; set; }
}
