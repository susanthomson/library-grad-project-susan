package com.scottlogic.librarysusan;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import waffle.servlet.spi.NegotiateSecurityFilterProvider;
import waffle.servlet.spi.SecurityFilterProvider;
import waffle.servlet.spi.SecurityFilterProviderCollection;
import waffle.spring.NegotiateSecurityFilter;
import waffle.spring.NegotiateSecurityFilterEntryPoint;
import waffle.windows.auth.impl.WindowsAuthProviderImpl;

import java.util.ArrayList;
import java.util.List;

@Configuration
public class BeanBuilder {
    @Bean
    public WindowsAuthProviderImpl waffleWindowsAuthProvider() {
        return new WindowsAuthProviderImpl();
    }

    @Bean
    @Autowired
    public NegotiateSecurityFilterProvider negotiateSecurityFilterProvider(
            final WindowsAuthProviderImpl windowsAuthProvider) {
        return new NegotiateSecurityFilterProvider(windowsAuthProvider);
    }

    @Bean
    @Autowired
    public SecurityFilterProviderCollection waffleSecurityFilterProviderCollection(
            final NegotiateSecurityFilterProvider negotiateSecurityFilterProvider) {

        final List<SecurityFilterProvider> securityFilterProviders = new ArrayList<>();

        securityFilterProviders.add(negotiateSecurityFilterProvider);

        return new SecurityFilterProviderCollection(securityFilterProviders.toArray(new SecurityFilterProvider[] {}));
    }

    @Bean
    @Autowired
    public NegotiateSecurityFilterEntryPoint negotiateSecurityFilterEntryPoint(
            final SecurityFilterProviderCollection securityFilterProviderCollection) {

        final NegotiateSecurityFilterEntryPoint negotiateSecurityFilterEntryPoint = new NegotiateSecurityFilterEntryPoint();

        negotiateSecurityFilterEntryPoint.setProvider(securityFilterProviderCollection);

        return negotiateSecurityFilterEntryPoint;
    }

    @Bean
    @Autowired
    public NegotiateSecurityFilter waffleNegotiateSecurityFilter(
            final SecurityFilterProviderCollection securityFilterProviderCollection) {

        final NegotiateSecurityFilter negotiateSecurityFilter = new NegotiateSecurityFilter();

        negotiateSecurityFilter.setProvider(securityFilterProviderCollection);

        return negotiateSecurityFilter;
    }
}
