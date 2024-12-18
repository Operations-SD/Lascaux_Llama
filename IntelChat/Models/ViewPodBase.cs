using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ViewPodBase
{
    public int PodId { get; set; }

    public int LocationIdFk { get; set; }

    public string LocationLabel { get; set; } = null!;

    public string LocationDescription { get; set; } = null!;

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string PodPypeDdChan { get; set; } = null!;
}
