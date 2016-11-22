using LibraryGradProject.Models;
using LibraryGradProject.DAL;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class ReservationDBRepository : IReservationRepository<Reservation, Book, User>
    {
        private ILibraryContext db;

        public ReservationDBRepository(ILibraryContext db)
        {
            this.db = db;
        }
        public void Borrow(Book book, User user)
        {
            Reservation bookIsOut = db.Reservations
                .Where(reservation => reservation.BookId == book.Id && reservation.EndDate == null).SingleOrDefault();
            if (bookIsOut == null)
            {
                Add(new Reservation()
                {
                    BookId = book.Id,
                    UserId = GetUserId(user.Name),
                    StartDate = System.DateTime.Now.ToString()
                });
            }
            else
            {
                throw new System.InvalidOperationException("You cannot borrow a book that is already borrowed");
            }
        }

        public void Return(Book book, User user)
        {
            Reservation reservationToReturn = db.Reservations
                .Where(reservation => reservation.BookId == book.Id && reservation.EndDate == null).SingleOrDefault();
            if (reservationToReturn != null)
            {
                var userId = GetUserId(user.Name);
                if (reservationToReturn.UserId != userId)
                {
                    throw new System.InvalidOperationException("You cannot return a book that someone else borrowed");
                }
                else
                {
                    reservationToReturn.EndDate = System.DateTime.Now.ToString();
                    db.SaveChanges();
                }
            }
            else
            {
                throw new System.InvalidOperationException("You cannot return a book that has not been borrowed");
            }
        }

        public void Add(Reservation entity)
        {
            db.Reservations.Add(entity);
            db.SaveChanges();
        }

        public IEnumerable<Reservation> GetAll()
        {
            return db.Reservations.ToList();
        }

        public Reservation Get(int id)
        {
            return db.Reservations.Where(reservation => reservation.Id == id).SingleOrDefault();
        }

        private int GetUserId(string name)
        {
            User foundUser = db.Users
                .Where(user => user.Name == name).SingleOrDefault();
            if (foundUser != null)
            {
                return foundUser.Id;
            } else
            {
                var addedUser = db.Users.Add(new User() { Name = name });
                return addedUser.Id;
            }
        }
    }
}