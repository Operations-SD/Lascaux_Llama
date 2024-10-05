using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class XnovaDictionaryNounObject
{
    public string About { get; set; } = null!;

    public int P { get; set; }

    public int? N { get; set; }

    public string? NovaDescription { get; set; }

    public int? S { get; set; }

    public string? Subject { get; set; }

    public string? SubjectDescription { get; set; }

    public string? SubjectUrl { get; set; }

    public int? A { get; set; }

    public string? Action { get; set; }

    public string? ActionDescription { get; set; }

    public string? ActionUrl { get; set; }

    public int? O { get; set; }

    public string? Object { get; set; }

    public string? ObjectDescription { get; set; }

    public string? ObjectUrl { get; set; }
}
