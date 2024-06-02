namespace DermDiag.Models
{
    public class Review
    {

        public string? Feedback { get; set; }
        public int Rate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
