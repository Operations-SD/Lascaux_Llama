using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Element
{
    public int ElementId { get; set; }

    public string ElementLabel16 { get; set; } = null!;

    public string ElementType { get; set; } = null!;

    public string ElementStatus { get; set; } = null!;
}
