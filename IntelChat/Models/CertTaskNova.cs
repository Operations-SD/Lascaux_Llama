using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CertTaskNova
{
    public string NovaDescription { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public string TaskStatus { get; set; } = null!;

    public string TaskDescription { get; set; } = null!;

    public int TaskPrevious { get; set; }

    public int TaskParent { get; set; }

    public int NovaId { get; set; }
}
