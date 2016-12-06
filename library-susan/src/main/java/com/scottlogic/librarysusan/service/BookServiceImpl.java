package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.dao.BookRepository;
import com.scottlogic.librarysusan.domain.Book;

import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class BookServiceImpl implements BookService{

    private final BookRepository bookRepository;

    public BookServiceImpl(final BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }

    @Override
    public void add(final Book book) {
        bookRepository.save(book);
    }

    @Override
    public Iterable<Book> getBooks() {
        return bookRepository.findAll();
    }
}