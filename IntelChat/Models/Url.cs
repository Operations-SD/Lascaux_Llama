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
}
