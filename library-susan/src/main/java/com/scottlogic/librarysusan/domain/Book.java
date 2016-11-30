package com.scottlogic.librarysusan.domain;

import javax.persistence.*;

@Entity
public class Book {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String isbn;
    private String title;
    private String author;
    private String publishDate;
    private String coverImage;

    public Book() {
        //empty constructor required by hibernate
    }

    public Book(String isbn, String title, String author, String publishDate, String coverImage) {
        this.isbn = isbn;
        this.title = title;
        this.author = author;
        this.publishDate = publishDate;
        this.coverImage = coverImage;
    }

    public Integer getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getIsbn() {
        return isbn;
    }

    public void setIsbn(String isbn) {
        this.isbn = isbn;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public String getPublishDate() {
        return publishDate;
    }

    public void setPublishDate(String publishDate) {
        this.publishDate = publishDate;
    }

    public String getCoverImage() {
        return coverImage;
    }

    public void setCoverImage(String coverImage) {
        this.coverImage = coverImage;
    }
}

