package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.dao.ReservationRepository;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.domain.Book;

import org.springframework.stereotype.Service;
import java.util.Optional;

import java.util.Date;
import java.text.SimpleDateFormat;

import javax.persistence.EntityNotFoundException;

@Service
public class ReservationServiceImpl implements ReservationService{

    private final ReservationRepository reservationRepository;

    public ReservationServiceImpl(final ReservationRepository reservationRepository) {
        this.reservationRepository = reservationRepository;
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
    public void borrow(final Book book) {
        Optional<Reservation> reserved = reservationRepository.findByBookAndEndDateIsNull(book);
        if (reserved.isPresent()) {
            throw new EntityNotFoundException("You cannot borrow a book that is already borrowed");
        } else {
            Date now = new Date();
            SimpleDateFormat formatter = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
            String startDate = formatter.format(now);
            final Reservation reservation = new Reservation(book, "0", startDate, null);
            reservationRepository.save(reservation);
        }
    }

}
