namespace LibraryGradProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}