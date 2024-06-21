namespace DermDiag.DTO
{
    public class MeetingDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string PatientLink { get; set; }
        public string DoctorLink { get; set; }

    }
}
