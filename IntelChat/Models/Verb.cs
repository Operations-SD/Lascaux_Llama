using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Verb
{
    public int VerbId { get; set; }

    public string VerbLabel { get; set; } = null!;

    public string VerbDescription { get; set; } = null!;

    public string VerbType { get; set; } = null!;

    public string VerbStatus { get; set; } = null!;

    public int PodIdFk { get; set; }

    public int UrlIdPk { get; set; }

    public string VerbTag { get; set; } = null!;

    public int VerbCategory { get; set; }
}
