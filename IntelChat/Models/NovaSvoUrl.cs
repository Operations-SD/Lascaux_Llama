using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class NovaSvoUrl
{
    public int PodIdFk { get; set; }

    public int NovaId { get; set; }

    public string NovaType { get; set; } = null!;

    public string NovaDescription { get; set; } = null!;

    public string NounLabel { get; set; } = null!;

    public string NounType { get; set; } = null!;

    public string VerbLabel { get; set; } = null!;

    public string VerbType { get; set; } = null!;

    public string Expr1 { get; set; } = null!;

    public string Expr2 { get; set; } = null!;
}
