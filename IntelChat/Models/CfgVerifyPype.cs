using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgVerifyPype
{
    public int PodId { get; set; }

    public string PypeId { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string DropDown { get; set; } = null!;

    public string PodLockNoun { get; set; } = null!;

    public string PodLockVerb { get; set; } = null!;

    public string PodLabel { get; set; } = null!;

    public string PypeStatus { get; set; } = null!;

    public string PypeDesc { get; set; } = null!;

    public string PypeLink { get; set; } = null!;
}
