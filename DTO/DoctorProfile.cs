namespace DermDiag.DTO
{
    public class DoctorProfile
    {
        public string? Name { get; set; }

        public decimal? Fees { get; set; }

        public int? NoReviews { get; set; }

        public float? Rating { get; set; }

        public int? NoSessions { get; set; }

        public string? Image { get; set; }

        public bool IsFavourite { get; set; }
    }
}
