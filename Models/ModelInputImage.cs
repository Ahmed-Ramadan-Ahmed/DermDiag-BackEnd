using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class ModelInputImage
{
    public int? ModelHistoryId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual PatientModelHistory? ModelHistory { get; set; }
}
