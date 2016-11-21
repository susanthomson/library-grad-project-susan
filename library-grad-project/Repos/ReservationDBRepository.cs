﻿using LibraryGradProject.Models;
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
                    UserId = user.Id,
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
                if (reservationToReturn.UserId != user.Id)
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
    }
}