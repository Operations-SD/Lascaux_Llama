using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CountNounType
{
    public int Pod { get; set; }

    public string Type { get; set; } = null!;

    public int? TypeCnt { get; set; }
}
