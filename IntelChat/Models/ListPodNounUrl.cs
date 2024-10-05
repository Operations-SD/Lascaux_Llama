using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ListPodNounUrl
{
    public int PodIdFk { get; set; }

    public string PodLabel { get; set; } = null!;

    public string NounType { get; set; } = null!;

    public int NounId { get; set; }

    public string NounLabel { get; set; } = null!;

    public string UrlCloud { get; set; } = null!;
}
