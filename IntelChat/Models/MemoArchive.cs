using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MemoArchive
{
    public int MemoPersonTo { get; set; }

    public int MemoPersonFrom { get; set; }

    public DateTime MemoDateTime { get; set; }

    public byte MemoPriority { get; set; }

    public int MemoPod { get; set; }

    public int MemoNova { get; set; }

    public int MemoChannel { get; set; }

    public string MemoType { get; set; } = null!;

    public string MemoStatus { get; set; } = null!;

    public string MemoMessage { get; set; } = null!;
}
