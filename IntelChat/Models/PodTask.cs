using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodTask
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public int NovaIdFk { get; set; }
}
