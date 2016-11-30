package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.Book;
import org.springframework.data.repository.CrudRepository;

import java.util.Optional;

public interface BookRepository extends CrudRepository<Book, Integer> {
    Optional<Book> findByAuthor(final String author);
}
