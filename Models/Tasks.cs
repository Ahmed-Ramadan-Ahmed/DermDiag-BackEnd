namespace DermDiag.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }

        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public int RepeatingDays { get; set; }
    }
}
