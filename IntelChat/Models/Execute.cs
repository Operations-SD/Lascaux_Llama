using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Execute
{
    public int ExecId { get; set; }

    public string WorkTypeStatusE { get; set; } = null!;

    public string? ExecuteText { get; set; }

    public int? GuideIdFk { get; set; }

    public byte? ExecuteR { get; set; }

    public byte? ExecuteS { get; set; }

    public int? Question { get; set; }

    public string? Role { get; set; }
}
