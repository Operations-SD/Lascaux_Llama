using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PypeNoun
{
    public string NounType { get; set; } = null!;

    public string NounLabel { get; set; } = null!;

    public int NounId { get; set; }

    public string NounDescription { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string PypeId { get; set; } = null!;

    public string PypeLabel { get; set; } = null!;

    public int PodIdFk { get; set; }
}
