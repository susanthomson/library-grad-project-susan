package com.scottlogic.librarysusan.dao;

import com.scottlogic.librarysusan.domain.User;
import org.springframework.data.repository.CrudRepository;

import java.util.Optional;

public interface UserRepository extends CrudRepository<User, Integer> {
    Optional<User> findByName(final String name);
}