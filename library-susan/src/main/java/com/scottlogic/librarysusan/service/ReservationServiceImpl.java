package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.dao.ReservationRepository;
import com.scottlogic.librarysusan.domain.Reservation;

import org.springframework.stereotype.Service;

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
}
