using DermDiag.Models;

namespace DermDiag.DTO
{
    public class TaskDTO
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }

        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public int RepeatingDays { get; set; }
    }
}
