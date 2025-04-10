using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class HsNovaVerb
{
    public int NovaId { get; set; }

    public string NovaDescription { get; set; } = null!;

    public string NovaType { get; set; } = null!;

    public string NovaStatus { get; set; } = null!;

    public int NovaSubject { get; set; }

    public int NovaAction { get; set; }

    public int NovaObject { get; set; }

    public int PodIdFk { get; set; }

    public string NovaTag { get; set; } = null!;

    public int VerbId { get; set; }

    public string VerbLabel { get; set; } = null!;
}
