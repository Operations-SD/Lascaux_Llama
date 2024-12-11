using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class NounTest
{
    public int NounId { get; set; }

    public string NounLabel { get; set; } = null!;

    public string NounDescription { get; set; } = null!;

    public string NounType { get; set; } = null!;

    public string NounStatus { get; set; } = null!;

    public int PodIdFk { get; set; }

    public int UrlIdPk { get; set; }

    public string NounTag { get; set; } = null!;

    public int NounCommon { get; set; }
}
