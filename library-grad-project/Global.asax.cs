using Autofac;
using Autofac.Integration.WebApi;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProject.DAL;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
            builder.RegisterType<UserDBRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<LibraryContext>().As<ILibraryContext>().InstancePerRequest();

            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
                {
                    //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://192.168.36.16:3000");
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT");
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                    HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "60");
                    HttpContext.Current.Response.End();
                }
            }
        }
    }
}
