using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgVerifyPype
{
    public int PodId { get; set; }

    public string LockNoun { get; set; } = null!;

    public string LockVerb { get; set; } = null!;

    public string PodLabel { get; set; } = null!;

    public string PypeId { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string DropDown { get; set; } = null!;

    public int Verified { get; set; }
}
