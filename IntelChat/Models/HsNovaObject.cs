using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class HsNovaObject
{
    public int NovaId { get; set; }

    public string NovaDescription { get; set; } = null!;

    public string NovaType { get; set; } = null!;

    public string NovaStatus { get; set; } = null!;

    public int NovaChannel { get; set; }

    public int NovaSubject { get; set; }

    public int NovaAction { get; set; }

    public int NovaObject { get; set; }

    public string NounLabel { get; set; } = null!;

    public int PodIdFk { get; set; }
}
