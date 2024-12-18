using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodPersonLocation
{
    public int PodId { get; set; }

    public string PodPypeDdPods { get; set; } = null!;

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string LocationDescription { get; set; } = null!;

    public string PersonLast { get; set; } = null!;
}
