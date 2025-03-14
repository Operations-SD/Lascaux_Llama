using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MapPod
{
    public int PersonId { get; set; }

    public string PersonFirst { get; set; } = null!;

    public string PersonLabel { get; set; } = null!;

    public string PersonPypeDdMyme { get; set; } = null!;

    public string PersonStatus { get; set; } = null!;

    public string PersonPypeDdRole { get; set; } = null!;
}
