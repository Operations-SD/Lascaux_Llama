using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LpElement
{
    public int LpElemId { get; set; }

    public string LpElemLabel { get; set; } = null!;

    public string LpElemDesc { get; set; } = null!;

    public string LpElemType { get; set; } = null!;

    public string LpElemStatus { get; set; } = null!;

    public int? LpMajorId { get; set; }

    public DateTime? LpElemDtime { get; set; }

    public virtual LpMinor? LpMajor { get; set; }
}
