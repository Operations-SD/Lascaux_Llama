﻿using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PersonTemp
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLast { get; set; } = null!;

    public string PersonLabel { get; set; } = null!;

    public string PersonType { get; set; } = null!;

    public string PersonStatus { get; set; } = null!;

    public string PersonRole { get; set; } = null!;

    public DateTime PersonDatetime { get; set; }
}
