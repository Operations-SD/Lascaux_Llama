﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class FilterPype
{
    public int PodIdFk { get; set; }

    public string PodLabel { get; set; } = null!;

    public string PypeType { get; set; } = null!;

    public string PypeLabel { get; set; } = null!;

    public string PypeStatus { get; set; } = null!;

    public string PypeDesc { get; set; } = null!;
}
