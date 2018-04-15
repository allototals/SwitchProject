using StructureMap;
using Switch.Data.Models;
using Switch.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using StructureMap.Configuration.DSL;
using Switch.Service;
using Switch.Data.Interface;
using Switch.Data.Concrete;

namespace Switch.UI
{

    public class Starter : System.Web.HttpApplication 
    {
        protected void Application_Start()
        {
            var container = new Container(c =>
            {
                c.For(typeof(IGenericService<>)).Use(typeof(GenericService<>));
                c.For(typeof(IInterface)).Use(typeof(SessionManager<>));
               // c.AddRegistry<GenericService<>>();

            });

            container.GetInstance<IInterface>();
            //ServiceBootrapper.Initialise();
            //var container = new StructureMap.Container(c =>
            //{
            //    c.AddRegistry<Switch.Service.GenericService>();
            //});
            //var container= new Container( _=>
            //    {
            //    _.For<(typeof(IGenericService<>))>().Use<typeof(Switch.Service.GenericService<>)>();
            //    });

            //container.GetInstance<IFoo>()
            //    // should be type Foo
            //    .ShouldBeOfType<Foo>()

            //    // and the IBar dependency too
            //    .Bar.ShouldBeOfType<Foo>();
        }
            //ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            //ObjectFactory.Initialize(x =>
            //{
            //    x.For<ISessionFactory>()
            //        .Singleton()
            //        .Use(CreateSessionFactory());

            //    x.For<ISession>()
            //        .HttpContextScoped()
            //        .Use(context => context.GetInstance<ISessionFactory>().OpenSession());
            //});

            //AreaRegistration.RegisterAllAreas();

            //RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterRoutes(RouteTable.Routes);
        }

        //protected void Application_EndRequest()
        //{
        //   // ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        //}
        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        "Default",
        //        "{controller}/{action}/{id}",
        //        new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //    );
        //}
    
}
