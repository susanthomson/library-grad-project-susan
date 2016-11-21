using Newtonsoft.Json;
using System.Collections.Generic;

namespace LibraryGradProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}