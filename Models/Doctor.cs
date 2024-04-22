using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public decimal? Fees { get; set; }

    public string? Description { get; set; }

    public int? NoReviews { get; set; }

    public int? NoSessions { get; set; }

    public string? Image { get; set; }

    public bool? AcceptanceStatus { get; set; }

    public virtual ICollection<Consulte> Consultes { get; set; } = new List<Consulte>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
