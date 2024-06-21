using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class Book
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int PaymentId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Payment? Payment { get; set; }
}
