package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.Book;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.autoconfigure.orm.jpa.TestEntityManager;
import org.springframework.test.context.junit4.SpringRunner;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.containsInAnyOrder;
import static org.hamcrest.Matchers.equalTo;

@RunWith(SpringRunner.class)
@DataJpaTest
public class BookRepositoryIT {

    @Autowired
    private BookRepository bookRepository;

    @Autowired
    private TestEntityManager entityManager;


    @Test
    public void shouldAddBook(){
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");

        bookRepository.save(book);

        final Book result = entityManager.find(Book.class, book.getId());
        assertThat(result, equalTo(book));
    }

    @Test
    public void shouldGetBooks(){
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");
        final Book otherBook = new Book("8", "A Better Book", "Some Other Guy", "12 May 1980", "better picture of a horse");

        bookRepository.save(book);
        bookRepository.save(otherBook);

        Iterable<Book> books = bookRepository.findAll();
        assertThat(books, containsInAnyOrder(book, otherBook));

    }
}
