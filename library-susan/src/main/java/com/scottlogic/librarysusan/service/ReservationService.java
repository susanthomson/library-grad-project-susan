package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.domain.Reservation;

public interface ReservationService {
    void add(final Reservation reservation);
    Iterable<Reservation> getReservations();
}
