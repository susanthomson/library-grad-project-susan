package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.Book;
import org.springframework.data.repository.CrudRepository;

public interface BookRepository extends CrudRepository<Book, Integer> {
}
