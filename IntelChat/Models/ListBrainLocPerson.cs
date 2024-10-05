using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ListBrainLocPerson
{
    public string BrainLabel { get; set; } = null!;

    public string BrainDescription { get; set; } = null!;

    public string BrainType { get; set; } = null!;

    public string BrainConnectionString { get; set; } = null!;

    public string BrainStorage { get; set; } = null!;

    public string LocationLabel { get; set; } = null!;

    public string LocationDescription { get; set; } = null!;

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;
}
