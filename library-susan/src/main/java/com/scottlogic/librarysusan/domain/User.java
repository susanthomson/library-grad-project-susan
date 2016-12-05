package com.scottlogic.librarysusan.domain;

import javax.persistence.*;
import com.fasterxml.jackson.annotation.*;

@Entity
@Table(name = "Users")
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @JsonProperty("Id")
    @Column(name="Id")
    private Integer id;

    @JsonProperty("Name")
    @Column(name="Name")
    private String name;

    public User() {
        //empty constructor required by hibernate
    }

    public User(String name) {
        this.name = name;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
