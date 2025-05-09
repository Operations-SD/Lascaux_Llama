﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class RegistrationRole
{
    public int RegistrationId { get; set; }

    public string RegistrationUsername { get; set; } = null!;

    public string PersonPypeDdMyme { get; set; } = null!;

    public string PersonPypeDdRole { get; set; } = null!;
}
