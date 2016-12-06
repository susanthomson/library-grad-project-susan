package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.Book;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.domain.User;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.autoconfigure.orm.jpa.TestEntityManager;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Optional;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.containsInAnyOrder;
import static org.hamcrest.Matchers.equalTo;

@RunWith(SpringRunner.class)
@DataJpaTest
public class ReservationRepositoryIT {

    @Autowired
    private ReservationRepository reservationRepository;
    @Autowired
    private BookRepository bookRepository;
    @Autowired
    private UserRepository userRepository;

    @Autowired
    private TestEntityManager entityManager;

    @Test
    public void shouldGetReservations(){
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");
        bookRepository.save(book);
        final User user = new User("dave");
        userRepository.save(user);

        final Reservation reservation = new Reservation(book, user,"today", null);
        final Reservation otherReservaton = new Reservation(book, user,"yesterday", "today");

        reservationRepository.save(reservation);
        reservationRepository.save(otherReservaton);

        Iterable<Reservation> reservations = reservationRepository.findAll();
        assertThat(reservations, containsInAnyOrder(reservation, otherReservaton));

    }

    @Test
    public void shouldFindReservedBook(){
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");
        bookRepository.save(book);
        final User user = new User("dave");
        userRepository.save(user);

        final Reservation reservation = new Reservation(book, user,"today", null);
        final Reservation otherReservaton = new Reservation(book, user,"yesterday", "today");

        reservationRepository.save(reservation);
        reservationRepository.save(otherReservaton);

        Optional<Reservation> reserved  = reservationRepository.findByBookAndEndDateIsNull(book);
        assertThat(reserved.get(), equalTo(reservation));

    }
}
