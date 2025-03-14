using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class CfgProgramGuide
{
    public string ProgramLabel { get; set; } = null!;

    public int ProgramIdFk { get; set; }

    public int GuideIview { get; set; }

    public string GuideLabel { get; set; } = null!;
}
