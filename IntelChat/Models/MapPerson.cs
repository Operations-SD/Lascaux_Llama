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
}
