using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MemoLink
{
    public int To { get; set; }

    public string Receive { get; set; } = null!;

    public int From { get; set; }

    public string Sender { get; set; } = null!;

    public byte Priority { get; set; }

    public string Open { get; set; } = null!;

    public DateTime Time { get; set; }

    public string Message { get; set; } = null!;

    public string? LocationTo { get; set; }

    public string? About { get; set; }
}
