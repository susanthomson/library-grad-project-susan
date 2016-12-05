package com.scottlogic.librarysusan.service;

import com.scottlogic.librarysusan.dao.UserRepository;
import com.scottlogic.librarysusan.domain.User;

import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class UserServiceImpl implements UserService{

    private final UserRepository userRepository;

    public UserServiceImpl(final UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public int getUserId(final String username) {
        Optional<User> foundUser = userRepository.findByName(username);
        if (foundUser.isPresent()) {
            return foundUser.get().getId();
        } else {
            User addedUser = userRepository.save(new User(username));
            return addedUser.getId();
        }
    }

}
