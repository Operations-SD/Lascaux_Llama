using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Url
{
    public int UrlId { get; set; }

    public string UrlLabel { get; set; } = null!;

    public string UrlDescription { get; set; } = null!;

    public string UrlType { get; set; } = null!;

    public string UrlStatus { get; set; } = null!;

    public string UrlCloud { get; set; } = null!;

    public byte UrlAdvanceLevel { get; set; }

    public int NovaIdFk { get; set; }

    public string UrlTag { get; set; } = null!;

    public int UrlDaisey { get; set; }
}
