using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MapPerson
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public string PersonLabel { get; set; } = null!;

    public string PersonPypeDdMyme { get; set; } = null!;

    public string PersonStatus { get; set; } = null!;

    public string PersonPypeDdRole { get; set; } = null!;

    public DateTime PersonDatetime { get; set; }

    public string? PersonMyCloud { get; set; }

    public string PersonTag { get; set; } = null!;

    public int? LocationIdFk { get; set; }

    public int? ProgramIdFk { get; set; }

    public string GuideIdFk { get; set; } = null!;

    public int? NovaIdFk { get; set; }

    public int UrlIdFk { get; set; }

    public int? PodIdFk { get; set; }

    public byte PersonEligible { get; set; }

    public int BrainIdFk { get; set; }
}
