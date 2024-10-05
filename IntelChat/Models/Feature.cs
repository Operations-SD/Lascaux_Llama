using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Feature
{
    public int FeatureId { get; set; }

    public string FeatureLabel { get; set; } = null!;

    public string FeatureType { get; set; } = null!;

    public int ExternalPk { get; set; }

    public string FeatureStatus { get; set; } = null!;

    public int FeatureInteger { get; set; }

    public double FeatureReal { get; set; }

    public int NounFk { get; set; }
}
