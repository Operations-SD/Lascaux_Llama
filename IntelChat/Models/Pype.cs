﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Pype
{
    public string PypeId { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string PypeLabel { get; set; } = null!;

    public string PypeStatus { get; set; } = null!;

    public string PypeDesc { get; set; } = null!;

    public string PypeLink { get; set; } = null!;

    public int PodIdFk { get; set; }
}
