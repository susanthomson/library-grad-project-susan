using LibraryGradProject.Models;
using System.Data.Entity;

namespace LibraryGradProject.DAL
{
    public interface ILibraryContext
    {

        DbSet<Book> Books { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        int SaveChanges();
    }
}