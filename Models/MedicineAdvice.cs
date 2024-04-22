using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class MedicineAdvice
{
    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public string? MedicineName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? Frequency { get; set; }

    public int? Quantity { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
