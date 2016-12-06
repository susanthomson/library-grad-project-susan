using Newtonsoft.Json;
using System.Collections.Generic;

namespace LibraryGradProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
        public string CoverImage { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}