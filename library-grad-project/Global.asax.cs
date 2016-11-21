using Autofac;
using Autofac.Integration.WebApi;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProject.DAL;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;

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
            builder.RegisterType<BookDBRepository>().As<IBookRepository<Book>>().InstancePerRequest();
            builder.RegisterType<ReservationDBRepository>().As<IReservationRepository<Reservation, Book, User>>().InstancePerRequest();
            builder.RegisterType<LibraryContext>().As<ILibraryContext>().InstancePerRequest();

            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
