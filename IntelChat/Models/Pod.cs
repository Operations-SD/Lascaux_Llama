using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Pod
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string PodType { get; set; } = null!;

    public string PodStatus { get; set; } = null!;

    public string PodChannel { get; set; } = null!;

    public string PodUrlBase { get; set; } = null!;

    public int PersonIdFk { get; set; }

    public int LocationIdFk { get; set; }

    public int NovaIdFk { get; set; }
}
