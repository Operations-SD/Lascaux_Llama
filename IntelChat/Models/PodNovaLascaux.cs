using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodNovaLascaux
{
    public int PodId { get; set; }

    public int NovaId { get; set; }

    public string NovaType { get; set; } = null!;

    public string NovaDescription { get; set; } = null!;

    public int SId { get; set; }

    public string SType { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public int VId { get; set; }

    public string VType { get; set; } = null!;

    public string Verb { get; set; } = null!;

    public int OId { get; set; }

    public string OType { get; set; } = null!;

    public string Object { get; set; } = null!;

    public int SGlyph { get; set; }

    public int VGlyph { get; set; }

    public int OGlyph { get; set; }
}
