using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class Consulte
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public bool? DoctorAttendance { get; set; }

    public bool? PatientAttendance { get; set; }

    public string? Status { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
