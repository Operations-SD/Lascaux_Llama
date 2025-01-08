using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Memo
{
    public int MemoPersonTo { get; set; }

    public int MemoPersonFrom { get; set; }

    public DateTime MemoDateTime { get; set; }

    public DateTime? MemoDtOriginal { get; set; }

    public byte MemoPriority { get; set; }

    public string MemoType { get; set; } = null!;

    public string MemoStatus { get; set; } = null!;

    public string MemoMessage { get; set; } = null!;

    public int MemoChannel { get; set; }

    public int PodIdFk { get; set; }

    public int GuideIdFk { get; set; }

    public int QuestionIdFk { get; set; }

    public int? MemoNova { get; set; }
}
