using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ListPodVerbUrl
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public int VerbId { get; set; }

    public string VerbLabel { get; set; } = null!;

    public string UrlLabel { get; set; } = null!;

    public string UrlCloud { get; set; } = null!;
}
