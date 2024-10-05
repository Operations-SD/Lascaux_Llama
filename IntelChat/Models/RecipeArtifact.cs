using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class RecipeArtifact
{
    public int Item { get; set; }

    public string Recipe { get; set; } = null!;

    public int Who { get; set; }

    public string Chef { get; set; } = null!;

    public int Period { get; set; }

    public string Classification { get; set; } = null!;

    public int What { get; set; }

    public string Category { get; set; } = null!;

    public int Where { get; set; }

    public string Location { get; set; } = null!;
}
