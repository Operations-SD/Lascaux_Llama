using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Tuner
{
    public string TunerChannel { get; set; } = null!;

    public byte TuberPushbtn { get; set; }

    public string TunerType { get; set; } = null!;

    public string TunerLabel { get; set; } = null!;

    public int TaskIdFk { get; set; }

    public int UrlIdFk { get; set; }
}
