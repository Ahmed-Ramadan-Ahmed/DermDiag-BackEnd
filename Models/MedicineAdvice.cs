using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class MedicineAdvice
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public string MedicineName { get; set; }

    public string Frequency { get; set; }

    public string Quantity { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual Patient Patient { get; set; }
}
