using LibraryGradProject.Models;
using System.Data.Entity;

namespace LibraryGradProject.DAL
{
    public class LibraryContext : DbContext, ILibraryContext
    {

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}