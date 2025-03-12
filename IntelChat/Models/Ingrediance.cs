using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Ingrediance
{
    public int NounIdFk { get; set; }

    public int ElementIdFk { get; set; }

    public string IngredMeasure { get; set; } = null!;

    public double IngredHowmuch { get; set; }
}
