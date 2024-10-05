using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Channel
{
    public int ChannelId { get; set; }

    public string? ChannelType { get; set; }

    public string ChannelLabel { get; set; } = null!;

    public string ChannelRole { get; set; } = null!;

    public int PodIdFk { get; set; }

    public int LocationIdFk { get; set; }

    public int PersonIdFk { get; set; }
}
