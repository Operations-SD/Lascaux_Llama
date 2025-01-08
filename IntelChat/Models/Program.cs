using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramLabel { get; set; } = null!;

    public string ProgramType { get; set; } = null!;

    public string ProgramStatus { get; set; } = null!;

    /// <summary>
    /// Physical Site
    /// </summary>
    public string ProgramDesc { get; set; } = null!;

    public string ProgramTag { get; set; } = null!;

    public int PersonIdFk { get; set; }
}
