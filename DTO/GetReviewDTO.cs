namespace DermDiag.DTO
{
    public class GetReviewDTO
    {
        public string Feedback { get; set; }
        public int Rate { get; set; }
        public string PatientName { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
