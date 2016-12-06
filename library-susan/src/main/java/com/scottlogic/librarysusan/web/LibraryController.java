package com.scottlogic.librarysusan.web;

import com.scottlogic.librarysusan.domain.Book;
import com.scottlogic.librarysusan.service.BookService;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.service.ReservationService;
import com.scottlogic.librarysusan.service.UserService;
import org.springframework.web.bind.annotation.*;

import javax.persistence.EntityNotFoundException;
import java.util.Optional;

@CrossOrigin
@RestController
public class LibraryController {
    private final BookService bookService;
    private final ReservationService reservationService;
    private final UserService userService;
    private final String username = "hardcoded username";

    public LibraryController(BookService bookService, ReservationService reservationService, UserService userService) {
        this.bookService = bookService;
        this.reservationService = reservationService;
        this.userService = userService;
    }

    @GetMapping(value = "/api/books")
    @ResponseBody
    public Iterable<Book> getBooks() {
        return bookService.getBooks();
    }

    @PostMapping(value = "/api/books")
    public void addBook(@RequestBody final Book book){
        bookService.add(book);
    }

    @GetMapping(value = "/api/reservations")
    @ResponseBody
    public Iterable<Reservation> getReservations() {
        return reservationService.getReservations();
    }

    @PostMapping(value = "/api/reservations")
    public void borrow(@RequestBody final Book book){
        reservationService.borrow(book, username);
    }

    @PutMapping(value = "/api/reservations")
    public void unborrow(@RequestBody final Book book){
        reservationService.unborrow(book, username);
    }

    @GetMapping(value = "/api/users")
    @ResponseBody
    public int getUserId() {
        return userService.getUserId(username);
    }
}
