using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PersonArchive
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public string PersonLabel { get; set; } = null!;

    public string PersonType { get; set; } = null!;

    public string PersonStatus { get; set; } = null!;

    public string PersonRole { get; set; } = null!;

    public DateTime PersonDatetime { get; set; }

    public string? PersonMyCloud { get; set; }

    public string? PersonTag { get; set; }

    public int? LocationIdFk { get; set; }

    public int? ProgramIdFk { get; set; }

    public int? PodIdFk { get; set; }

    public int? NovaIdFk { get; set; }

    public int BrainIdFk { get; set; }

    public byte PersonEligibility { get; set; }
}
