﻿using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class Patient
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public string? Name { get; set; }

    public string Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? Address { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Consulte> Consultes { get; set; } = new List<Consulte>();

    public virtual ICollection<PatientModelHistory> PatientModelHistories { get; set; } = new List<PatientModelHistory>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

    public virtual ICollection<Review> Reviews { get; set; } = null !;


}
