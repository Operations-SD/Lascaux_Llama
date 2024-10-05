using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Status
{
    public string StatusPype { get; set; } = null!;

    public string StatusType { get; set; } = null!;

    public string StatusLabel { get; set; } = null!;

    public string StatusStatus { get; set; } = null!;
}
