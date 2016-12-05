package com.scottlogic.librarysusan.service;


import com.scottlogic.librarysusan.dao.ReservationRepository;
import com.scottlogic.librarysusan.dao.UserRepository;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.domain.Book;

import com.scottlogic.librarysusan.domain.User;
import org.springframework.stereotype.Service;
import java.util.Optional;

import java.util.Date;
import java.text.SimpleDateFormat;

import javax.persistence.EntityNotFoundException;

@Service
public class ReservationServiceImpl implements ReservationService{

    private final ReservationRepository reservationRepository;
    private final UserRepository userRepository;

    public ReservationServiceImpl(ReservationRepository reservationRepository, UserRepository userRepository) {
        this.reservationRepository = reservationRepository;
        this.userRepository = userRepository;
    }

    @Override
    public void add(final Reservation reservation) {
        reservationRepository.save(reservation);
    }

    @Override
    public Iterable<Reservation> getReservations() {
        return reservationRepository.findAll();
    }

    @Override
    public void borrow(final Book book, final String username) {
        Optional<Reservation> reserved = reservationRepository.findByBookAndEndDateIsNull(book);
        if (reserved.isPresent()) {
            throw new EntityNotFoundException("You cannot borrow a book that is already borrowed");
        } else {
            Date now = new Date();
            SimpleDateFormat formatter = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
            String startDate = formatter.format(now);

            Optional<User> user = userRepository.findByName(username);
            User borrower;
            if (!user.isPresent()) {
                borrower = userRepository.save(new User(username));
            } else {
                borrower = user.get();
            }

            final Reservation reservation = new Reservation(book, borrower, startDate, null);
            reservationRepository.save(reservation);
        }
    }

    @Override
    public void unborrow(final Book book, final String username) {
        Optional<Reservation> reserved = reservationRepository.findByBookAndEndDateIsNull(book);
        if (reserved.isPresent()) {
            Optional<User> user = userRepository.findByName(username);
            if (user.isPresent()) {
                if (user.get().equals(reserved.get().getUser())) {
                    Date now = new Date();
                    SimpleDateFormat formatter = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
                    String endDate = formatter.format(now);
                    Reservation reservation = reserved.get();
                    reservation.setEndDate(endDate);
                    reservationRepository.save(reservation);
                } else {
                    throw new EntityNotFoundException("You cannot return a book that someone else borrowed");
                }
            } else {
                //user isn't in db so can't have borrowed book
                throw new EntityNotFoundException("You cannot return a book that someone else borrowed");
            }
        } else {
            throw new EntityNotFoundException("You cannot return a book that has not been borrowed");
        }
    }

}
