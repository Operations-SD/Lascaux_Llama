using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class RegistrationCfg
{
    public int RegistrationId { get; set; }

    public string RegistrationUsername { get; set; } = null!;

    public string RegistrationPassword { get; set; } = null!;

    public string RegistrationEmail { get; set; } = null!;

    public string RegistrationStatus { get; set; } = null!;

    public int PersonIdFk { get; set; }

    public DateTime RegistrationDate { get; set; }
}
