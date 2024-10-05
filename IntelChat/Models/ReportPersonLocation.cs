using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ReportPersonLocation
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public string PersonStatus { get; set; } = null!;

    public string PersonRole { get; set; } = null!;

    public int PodIdFk { get; set; }

    public string LocationType { get; set; } = null!;

    public string LocationLabel16 { get; set; } = null!;

    public string LocationDesc { get; set; } = null!;

    public int LocationId { get; set; }
}
