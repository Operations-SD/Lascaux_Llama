using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Command
{
    public int CommandInitiator { get; set; }

    public DateTime CommandDtime { get; set; }

    public string CommandType { get; set; } = null!;

    public string CommandLabel16 { get; set; } = null!;

    public string CommandStatus { get; set; } = null!;

    /// <summary>
    /// Physical Site
    /// </summary>
    public string CommandConnect { get; set; } = null!;

    public string CommandString { get; set; } = null!;

    public byte CommandResponse { get; set; }

    public byte CommandSeverity { get; set; }

    public int PersonIdFk { get; set; }

    public int NeuronIdFk { get; set; }

    public int NovaIdFk { get; set; }
}
