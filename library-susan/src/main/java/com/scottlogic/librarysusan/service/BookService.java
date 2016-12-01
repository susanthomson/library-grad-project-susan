package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.domain.Book;

import java.util.Optional;

public interface BookService {
    Optional<Book> getByAuthor(final String author);

    void add(final Book book);
    Iterable<Book> getBooks();
}
