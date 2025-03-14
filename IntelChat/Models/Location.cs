using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string LocationLabel16 { get; set; } = null!;

    public string LocationType { get; set; } = null!;

    public string LocationStatus { get; set; } = null!;

    /// <summary>
    /// Physical Site
    /// </summary>
    public string LocationDesc { get; set; } = null!;

    public int LocationTimeZone { get; set; }

    public string LocationStorage { get; set; } = null!;

    public string LocationPng { get; set; } = null!;

    public string LocationFinder { get; set; } = null!;

    public float Latitude { get; set; }

    public float Longitude { get; set; }

    public int BrainFk { get; set; }

    public int LicenseFk { get; set; }

    public int PodFk { get; set; }

    public int ProgramIdFk { get; set; }

    public int PersonFkAdmn { get; set; }

    public int PersonFkEngr { get; set; }

    public int PersonFkXprt { get; set; }

    public string LocationTag { get; set; } = null!;

    public int NovaIdFk { get; set; }
}
