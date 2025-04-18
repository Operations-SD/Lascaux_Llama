﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LpMinor
{
    public int LpMinorId { get; set; }

    public string LpMinorLabel { get; set; } = null!;

    public string LpMinorDesc { get; set; } = null!;

    public string LpMinorType { get; set; } = null!;

    public string LpMinorStatus { get; set; } = null!;

    public int LpMajorId { get; set; }

    public DateTime? LpMinorDtime { get; set; }

    public int? Location { get; set; }

    public short? LpMinorPersue { get; set; }

    public short? LpMinorAvoid { get; set; }
}
