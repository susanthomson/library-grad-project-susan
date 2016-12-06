package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.domain.Book;

import java.util.Optional;

public interface BookService {
    void add(final Book book);
    Iterable<Book> getBooks();
}
