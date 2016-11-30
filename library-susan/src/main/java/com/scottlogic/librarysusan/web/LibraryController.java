package com.scottlogic.librarysusan.web;

import com.scottlogic.librarysusan.domain.Book;
import com.scottlogic.librarysusan.service.BookService;
import org.springframework.web.bind.annotation.*;

import javax.persistence.EntityNotFoundException;
import java.util.Optional;

@RestController
public class LibraryController {
    private final BookService bookService;

    public LibraryController(final BookService bookService) {
        this.bookService = bookService;
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

    @PostMapping(value = "/books")
    public void addBook(@RequestBody final Book book){
        bookService.add(book);
    }
}
