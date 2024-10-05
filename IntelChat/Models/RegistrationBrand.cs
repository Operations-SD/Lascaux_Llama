using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class RegistrationBrand
{
    public int RegistrationId { get; set; }

    public string BrandRole { get; set; } = null!;

    public int PodIdFk { get; set; }
}
