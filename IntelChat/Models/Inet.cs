using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Inet
{
    public int InetId { get; set; }

    public string NetLabel { get; set; } = null!;

    public string InetType { get; set; } = null!;

    public string InetStatus { get; set; } = null!;

    public string InetContent { get; set; } = null!;

    public string InetLink { get; set; } = null!;

    public int InetImage { get; set; }

    public int NovaIdFk { get; set; }

    public short InetStartMin { get; set; }

    public short InetDurationMin { get; set; }

    public byte InetValue { get; set; }

    public int PodFk { get; set; }
}
