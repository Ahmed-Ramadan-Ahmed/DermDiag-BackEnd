﻿namespace DermDiag.DTO
{
    public class ViewDoctorDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }


        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public decimal? Fees { get; set; }

        public string? Description { get; set; }

        public int? NoReviews { get; set; }

        public float? Rating { get; set; }
        public int? NoSessions { get; set; }

        public string? Image { get; set; }

        public bool? AcceptanceStatus { get; set; }
    }
}
