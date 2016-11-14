﻿using Autofac;
using Autofac.Integration.WebApi;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace LibraryGradProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Autofac
            var builder = new ContainerBuilder();            

            // Register Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register types
            builder.RegisterType<FilledBookRepository>().As<IBookRepository<Book>>().SingleInstance();
            builder.RegisterType<ReservationRepository>().As<IReservationRepository<Reservation, Book>>().SingleInstance();

            //var reservationRepo = new ReservationRepository();
            //builder.RegisterInstance(reservationRepo).As<ReservationRepository>();

            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
