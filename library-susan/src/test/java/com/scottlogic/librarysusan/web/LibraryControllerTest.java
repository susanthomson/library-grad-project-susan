package com.scottlogic.librarysusan.web;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.scottlogic.librarysusan.domain.Book;
import com.scottlogic.librarysusan.domain.Reservation;
import com.scottlogic.librarysusan.service.*;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import java.util.Optional;

import static org.hamcrest.CoreMatchers.is;
import static org.mockito.Matchers.any;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.put;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@RunWith(SpringRunner.class)
@WebMvcTest(controllers = {LibraryController.class})
public class LibraryControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private BookService bookService;
    @MockBean
    private ReservationService reservationService;
    @MockBean
    private UserService userService;

    @Test
    public void shouldGetBooks() throws Exception {

        mockMvc.perform(get("/api/books"))
                .andExpect(status().isOk());

        verify(bookService).getBooks();
    }

    @Test
    public void shouldAddBook() throws Exception {
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");

        mockMvc.perform(post("/api/books")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(asJsonString(book)))
                .andExpect(status().isOk());

        verify(bookService).add(any(Book.class));
    }

    @Test
    public void shouldGetReservations() throws Exception {

        mockMvc.perform(get("/api/reservations"))
                .andExpect(status().isOk());

        verify(reservationService).getReservations();
    }

    @Test
    public void shouldBorrowBook() throws Exception {
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");

        mockMvc.perform(post("/api/reservations")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(asJsonString(book)))
                .andExpect(status().isOk());

        verify(reservationService).borrow(any(Book.class), eq("hardcoded username"));
    }

    @Test
    public void shouldUnborrowBook() throws Exception {
        final Book book = new Book("7", "A Good Book", "Some Guy", "03 January 1979", "picture of a horse");

        mockMvc.perform(put("/api/reservations")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(asJsonString(book)))
                .andExpect(status().isOk());

        verify(reservationService).unborrow(any(Book.class), eq("hardcoded username"));
    }

    @Test
    public void shouldGetUserId() throws Exception {

        mockMvc.perform(get("/api/users"))
                .andExpect(status().isOk());

        verify(userService).getUserId(eq("hardcoded username"));
    }

    public String asJsonString(final Object object) throws JsonProcessingException {
        final ObjectMapper objectMapper = new ObjectMapper();
        return objectMapper.writeValueAsString(object);
    }
}
