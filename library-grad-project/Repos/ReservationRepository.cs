using LibraryGradProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class ReservationRepository
    {
        private List<Reservation> _reservationCollection = new List<Reservation>();

        public void Borrow(Book book)
        {
            Reservation bookIsOut = _reservationCollection
                .Where(reservation => reservation.BookId == book.Id && reservation.EndDate == null).SingleOrDefault();
            if (bookIsOut == null)
            {
                Add(new Reservation()
                {
                    BookId = book.Id,
                    StartDate = System.DateTime.Now.ToString()
                });
            }
            else
            {
                throw new System.InvalidOperationException("You cannot borrow a book that is already borrowed");
            }
        }

        public void Return(Book book)
        {
            Reservation reservationToReturn = _reservationCollection
                .Where(reservation => reservation.BookId == book.Id && reservation.EndDate == null).SingleOrDefault();
            if (reservationToReturn != null)
            {
                reservationToReturn.EndDate = System.DateTime.Now.ToString();
            }
            else
            {
                throw new System.InvalidOperationException("You cannot return a book that has not been borrowed");
            }
        }

        public void Add(Reservation entity)
        {
            entity.Id = _reservationCollection.Count;
            _reservationCollection.Add(entity);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _reservationCollection;
        }

        public Reservation Get(int id)
        {
            return _reservationCollection.Where(reservation => reservation.Id == id).SingleOrDefault();
        }
    }
}