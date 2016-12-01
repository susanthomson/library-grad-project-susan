package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.Reservation;
import org.springframework.data.repository.CrudRepository;

import java.util.Optional;

public interface ReservationRepository extends CrudRepository<Reservation, Integer> {

}