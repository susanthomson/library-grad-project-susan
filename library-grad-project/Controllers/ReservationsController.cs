﻿using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Cors;
using System.Diagnostics;

namespace LibraryGradProject.Controllers
{
    public class ReservationsController : ApiController
    {
        private IReservationRepository<Reservation, Book, User> _reservationRepo;
        
        public ReservationsController(IReservationRepository<Reservation, Book, User> reservationRepository)
        {
            _reservationRepo = reservationRepository;
        }

        // GET api/reservations
        public IEnumerable<Reservation> Get()
        {
            return _reservationRepo.GetAll();
        }

        // GET api/reservations/{int}
        public Reservation Get(int id)
        {
            return _reservationRepo.Get(id);
        }

        // POST api/reservations
        public HttpStatusCodeResult Post(Book book)
        {
            try
            {
                var user = new User() { Name = User.Identity.Name };
                _reservationRepo.Borrow(book, user);
                return new HttpStatusCodeResult(200, "Book borrowed");
            }
            catch (InvalidOperationException e)
            {
                return new HttpStatusCodeResult(500, e.Message);
            }
        }

        // PUT api/reservations
        public HttpStatusCodeResult Put(Book book)
        {
            try
            {
                var user = new User() { Name = User.Identity.Name };
                _reservationRepo.Return(book, user);
                return new HttpStatusCodeResult(200, "Book returned");
            }
            catch (InvalidOperationException e)
            {
                return new HttpStatusCodeResult(500, e.Message);
            }
        }
    }
}
