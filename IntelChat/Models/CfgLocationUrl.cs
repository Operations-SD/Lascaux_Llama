using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgLocationUrl
{
    public string PodLabel { get; set; } = null!;

    public string LocationLabel16 { get; set; } = null!;

    public string NounLabel { get; set; } = null!;

    public string UrlLabel { get; set; } = null!;

    public string UrlDescription { get; set; } = null!;

    public string UrlType { get; set; } = null!;

    public string PodUrlBase { get; set; } = null!;

    public string UrlCloud { get; set; } = null!;

    public string Cloud { get; set; } = null!;

    public string? Urlconcat { get; set; }
}
