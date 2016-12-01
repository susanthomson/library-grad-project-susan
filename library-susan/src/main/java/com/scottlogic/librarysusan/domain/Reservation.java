package com.scottlogic.librarysusan.domain;

import javax.persistence.*;
import com.fasterxml.jackson.annotation.JsonProperty;

@Entity
public class Reservation {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @JsonProperty("Id")
    @Column(name="Id")
    private Integer id;

    @ManyToOne
    @JoinColumn(name = "BookId")
    @JsonProperty("BookId")
    private Book book;

    @JsonProperty("UserId")
    @Column(name="UserId")
    private String userId;

    @JsonProperty("startDate")
    @Column(name="startDate")
    private String startDate;

    @JsonProperty("EndDate")
    @Column(name="EndDate")
    private String endDate;

    public Reservation() {
        //empty constructor required by hibernate
    }

    public Reservation(Book book, String userId, String startDate, String endDate) {
        this.book = book;
        this.userId = userId;
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

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
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
