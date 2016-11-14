using LibraryGradProject.Models;
using System.Data.Entity;

namespace LibraryGradProject.DAL
{
    public class LibraryContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}