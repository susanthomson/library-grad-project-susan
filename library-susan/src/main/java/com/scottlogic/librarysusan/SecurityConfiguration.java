package com.scottlogic.librarysusan;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.web.authentication.www.BasicAuthenticationFilter;

import waffle.servlet.spi.NegotiateSecurityFilterProvider;
import waffle.servlet.spi.SecurityFilterProvider;
import waffle.servlet.spi.SecurityFilterProviderCollection;
import waffle.spring.NegotiateSecurityFilter;
import waffle.spring.NegotiateSecurityFilterEntryPoint;
import waffle.windows.auth.impl.WindowsAuthProviderImpl;


@Configuration
@EnableWebSecurity
public class SecurityConfiguration extends WebSecurityConfigurerAdapter {

    @Autowired
    private NegotiateSecurityFilterEntryPoint authenticationEntryPoint;

    @Autowired
    private NegotiateSecurityFilter securityFilter;

    @Override
    protected void configure(final HttpSecurity http) throws Exception {

        http.cors().and().authorizeRequests().anyRequest().authenticated().and().csrf().disable()
                .addFilterAfter(this.securityFilter, BasicAuthenticationFilter.class).exceptionHandling()
            .authenticationEntryPoint(this.authenticationEntryPoint);
    }
}
