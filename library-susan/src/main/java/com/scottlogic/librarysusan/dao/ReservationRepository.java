package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.domain.Book;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.Optional;

public interface ReservationRepository extends CrudRepository<Reservation, Integer>,
        JpaSpecificationExecutor<Reservation> {
    Optional<Reservation> findByBookAndEndDateIsNull(final Book book);
}