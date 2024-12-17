using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Brain
{
    public int BrainId { get; set; }

    public string BrainLabel { get; set; } = null!;

    public string BrainDescription { get; set; } = null!;

    public string BrainType { get; set; } = null!;

    public string BrainStatus { get; set; } = null!;

    public string BrainConnectionString { get; set; } = null!;

    public string BrainStorage { get; set; } = null!;

    public string BrainLogin { get; set; } = null!;

    public int LocationIdFk { get; set; }

    public int LanguageIdFk { get; set; }
}
