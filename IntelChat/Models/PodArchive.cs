﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodArchive
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public string PodDescription { get; set; } = null!;

    public string PodPypeDdPods { get; set; } = null!;

    public string PodStatus { get; set; } = null!;

    public string PodPypeDdChan { get; set; } = null!;

    public string PodImage { get; set; } = null!;

    public string PodPypeNoun { get; set; } = null!;

    public string PodPypeVerb { get; set; } = null!;

    public string PodTag { get; set; } = null!;

    public string PodPypeDdTime { get; set; } = null!;

    public int PersonIdFk { get; set; }

    public int LocationIdFk { get; set; }

    public int ProgramIdFk { get; set; }

    public int GuideIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public int UrlIdFk { get; set; }
}
