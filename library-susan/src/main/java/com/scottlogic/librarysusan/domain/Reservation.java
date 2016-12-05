package com.scottlogic.librarysusan.domain;

import javax.persistence.*;
import com.fasterxml.jackson.annotation.*;

@Entity
public class Reservation {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @JsonProperty("Id")
    @Column(name="Id")
    private Integer id;

    @ManyToOne
    @JoinColumn(name = "BookId")
    @JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "Id")
    @JsonIdentityReference(alwaysAsId = true)
    @JsonProperty("BookId")
    private Book book;

    @ManyToOne
    @JoinColumn(name = "UserId")
    @JsonIdentityInfo(generator = ObjectIdGenerators.PropertyGenerator.class, property = "Id")
    @JsonIdentityReference(alwaysAsId = true)
    @JsonProperty("UserId")
    private User user;

    @JsonProperty("StartDate")
    @Column(name="StartDate")
    private String startDate;

    @JsonProperty("EndDate")
    @Column(name="EndDate")
    private String endDate;

    public Reservation() {
        //empty constructor required by hibernate
    }

    public Reservation(Book book, User user, String startDate, String endDate) {
        this.book = book;
        this.user = user;
        this.startDate = startDate;
        this.endDate = endDate;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Book getBook() {
        return book;
    }

    public void setBook(Book book) {
        this.book = book;
    }

    public User getUser() {
        return user;
    }

    public void setUser(String userId) {
        this.user = user;
    }

    public String getStartDate() {
        return startDate;
    }

    public void setStartDate(String startDate) {
        this.startDate = startDate;
    }

    public String getEndDate() {
        return endDate;
    }

    public void setEndDate(String endDate) {
        this.endDate = endDate;
    }
}
