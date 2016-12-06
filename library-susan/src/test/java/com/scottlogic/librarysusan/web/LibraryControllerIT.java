package com.scottlogic.librarysusan.web;

import com.scottlogic.librarysusan.dao.BookRepository;
import com.scottlogic.librarysusan.dao.ReservationRepository;
import com.scottlogic.librarysusan.dao.UserRepository;
import com.scottlogic.librarysusan.domain.Book;
import org.apache.http.client.HttpClient;
import org.apache.http.conn.ssl.SSLConnectionSocketFactory;
import org.apache.http.conn.ssl.TrustSelfSignedStrategy;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.ssl.SSLContextBuilder;
import org.junit.After;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.http.client.HttpComponentsClientHttpRequestFactory;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.junit4.SpringRunner;

import java.security.KeyManagementException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.util.Optional;

import static org.hamcrest.CoreMatchers.equalTo;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.containsInAnyOrder;
import static org.hamcrest.beans.SamePropertyValuesAs.samePropertyValuesAs;
import static org.hamcrest.collection.IsIterableContainingInOrder.contains;

@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@ActiveProfiles("test")
public class LibraryControllerIT {

    @Autowired
    private TestRestTemplate restTemplate;

    @Autowired
    private BookRepository bookRepository;
    @Autowired
    private ReservationRepository reservationRepository;
    @Autowired
    private UserRepository userRepository;

    @Test
    public void shouldAddBook() throws NoSuchAlgorithmException, KeyStoreException, KeyManagementException{

        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");

        final ResponseEntity<Void> response = restTemplate.postForEntity("/api/books", book, Void.class);
        book.setId(1);

        assertThat(response.getStatusCode(), equalTo(HttpStatus.OK));
        Book addedBook = bookRepository.findOne(1);
        assertThat(addedBook, samePropertyValuesAs(book));
    }

}
