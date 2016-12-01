package com.scottlogic.librarysusan.web;

import com.scottlogic.librarysusan.domain.Book;
import com.scottlogic.librarysusan.service.BookService;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.service.ReservationService;
import org.springframework.web.bind.annotation.*;

import javax.persistence.EntityNotFoundException;
import java.util.Optional;

@RestController
public class LibraryController {
    private final BookService bookService;
    private final ReservationService reservationService;

    public LibraryController(BookService bookService, ReservationService reservationServiceService) {
        this.bookService = bookService;
        this.reservationService = reservationServiceService;
    }

    @GetMapping(value = "/books/{author}")
    @ResponseBody
    public Book getBook(@PathVariable("author") final String author){
        final Optional<Book> book = bookService.getByAuthor(author);
        if(book.isPresent()){
            return book.get();
        } else {
            throw new EntityNotFoundException("No book found by author " + author);
        }
    }

    @GetMapping(value = "/books")
    @ResponseBody
    public Iterable<Book> getBooks() {
        return bookService.getBooks();
    }

    @PostMapping(value = "/books")
    public void addBook(@RequestBody final Book book){
        bookService.add(book);
    }

    @GetMapping(value = "/reservations")
    @ResponseBody
    public Iterable<Reservation> getReservations() {
        return reservationService.getReservations();
    }

    @PostMapping(value = "/reservations")
    public void addReservation(@RequestBody final Reservation reservation){
        reservationService.add(reservation);
    }
}
