package com.scottlogic.librarysusan.web;

import com.scottlogic.librarysusan.domain.Book;
import com.scottlogic.librarysusan.service.BookService;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.service.ReservationService;
import org.springframework.web.bind.annotation.*;

import javax.persistence.EntityNotFoundException;
import java.util.Optional;

@CrossOrigin
@RestController
public class LibraryController {
    private final BookService bookService;
    private final ReservationService reservationService;

    public LibraryController(BookService bookService, ReservationService reservationServiceService) {
        this.bookService = bookService;
        this.reservationService = reservationServiceService;
    }

    @GetMapping(value = "/api/books/{author}")
    @ResponseBody
    public Book getBook(@PathVariable("author") final String author){
        final Optional<Book> book = bookService.getByAuthor(author);
        if(book.isPresent()){
            return book.get();
        } else {
            throw new EntityNotFoundException("No book found by author " + author);
        }
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
        reservationService.borrow(book);
    }

    @PutMapping(value = "/api/reservations")
    public void unborrow(@RequestBody final Book book){
        reservationService.unborrow(book);
    }
}
