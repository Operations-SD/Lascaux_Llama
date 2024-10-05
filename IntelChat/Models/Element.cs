using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Element
{
    public int ElementId { get; set; }

    public string ElementLabel { get; set; } = null!;

    public string ElementType { get; set; } = null!;

    public short ElementSeq { get; set; }

    public short ElementByte { get; set; }

    public int ElementInt { get; set; }

    public float ElementReal { get; set; }

    public int NounIdFk { get; set; }
}
