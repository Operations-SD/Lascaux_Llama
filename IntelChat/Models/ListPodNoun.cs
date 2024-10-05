using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ListPodNoun
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string NounType { get; set; } = null!;

    public int NounId { get; set; }

    public string NounLabel { get; set; } = null!;

    public string NounDescription { get; set; } = null!;

    public int UrlIdPk { get; set; }
}
