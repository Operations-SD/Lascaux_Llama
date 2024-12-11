using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ProgramGuide
{
    public int ProgramIdFk { get; set; }

    public int GuideIdFk { get; set; }
}
