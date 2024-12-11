using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgNovaSubject
{
    public int PodIdFk { get; set; }

    public int NovaId { get; set; }

    public int NovaSubject { get; set; }

    public string NounDescription { get; set; } = null!;

    public string NounType { get; set; } = null!;

    public string NounLabel { get; set; } = null!;

    public string UrlLabel { get; set; } = null!;

    public int UrlIdPk { get; set; }

    public string UrlCloud { get; set; } = null!;

    public int Verify { get; set; }

    public string PypeType { get; set; } = null!;
}
