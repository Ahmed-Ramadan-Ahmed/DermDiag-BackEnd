namespace DermDiag.DTO
{
    public class DoctorHomeDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Image { get; set; }
        public float? Rating { get; set; }
        public bool IsFavourite { get; set; }

    }
}
