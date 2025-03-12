using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ProgramGuide
{
    public int ProgramFk { get; set; }

    public int GuideIdFk { get; set; }
}
