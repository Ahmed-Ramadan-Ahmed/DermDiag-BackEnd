using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class PatientModelHistory
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public DateOnly? Date { get; set; }

    public string? ModelResult { get; set; }

    public virtual Patient? Patient { get; set; }
}
