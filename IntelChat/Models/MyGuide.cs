using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MyGuide
{
    public int PersonIdFk { get; set; }

    public int GuideIdFk { get; set; }

    public DateTime DateInitial { get; set; }

    public DateTime DateSelected { get; set; }

    public DateTime DateEvaluated { get; set; }
}
