using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgPod2
{
    public int PodIdFk { get; set; }

    public string PodPypeNoun { get; set; } = null!;

    public int NounId { get; set; }

    public string NounLabel { get; set; } = null!;

    public string NounDescription { get; set; } = null!;

    public string NounType { get; set; } = null!;

    public string NounStatus { get; set; } = null!;

    public int Expr1 { get; set; }

    public string PypeId { get; set; } = null!;
}
