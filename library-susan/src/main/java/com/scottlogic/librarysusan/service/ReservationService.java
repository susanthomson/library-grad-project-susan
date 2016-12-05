package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.domain.Book;

public interface ReservationService {
    void add(final Reservation reservation);
    Iterable<Reservation> getReservations();
    void borrow(final Book book);
    void unborrow(final Book book);
}
