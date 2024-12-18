using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LascauxNova
{
    public int PodIdFk { get; set; }

    public string PodLabel { get; set; } = null!;

    public int NovaId { get; set; }

    public string NovaDescription { get; set; } = null!;

    public string NovaType { get; set; } = null!;

    public string NovaStatus { get; set; } = null!;

    public int NounId { get; set; }

    public string NounLabel { get; set; } = null!;

    public int VerbId { get; set; }

    public string VerbLabel { get; set; } = null!;

    public int LascauxObjectId { get; set; }

    public string LascauxObject { get; set; } = null!;

    public string UrlLabel { get; set; } = null!;

    public string Expr3 { get; set; } = null!;

    public string Expr4 { get; set; } = null!;
}
