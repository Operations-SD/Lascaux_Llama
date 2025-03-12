using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgTaskUrl
{
    public int TaskId { get; set; }

    public string TaskLabel32 { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public short TaskPlatue { get; set; }

    public int PodIdFk { get; set; }

    public int? UrlId { get; set; }

    public string? UrlType { get; set; }

    public string? UrlLabel { get; set; }

    public string? UrlCloud { get; set; }
}
