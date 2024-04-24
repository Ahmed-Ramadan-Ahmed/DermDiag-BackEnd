namespace DermDiag.DTO
{
    public class PatientHomeDTO
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set; }

        public DateTime? AppointmentDate { get; set; }

    }
}
