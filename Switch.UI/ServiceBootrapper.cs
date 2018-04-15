using Microsoft.Practices.Unity;
using Project.Core.Data;
using Project.Core.EntityFramework;
using Project.Core.Validation;
using Switch.Data;
using Switch.Data.Interface;
using Switch.Data.Models;
using Switch.Service;
using Switch.Service.Interface;
using Switch.Service.Services;
using Switch.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI
{
    public class ServiceBootrapper
    {
        public static IUnityContainer Initialise()
        {

            var container = BuildUnityContainer();

            //Register Unity for MVC.
            // DependencyResolver.SetResolver(new UnityDependencyResolver(container));   
           // GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            // Register Unity for Web API
            // config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

          //  GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();  
            container.RegisterType<IObjectContextAdapter, Switch.Data.SwitchContext>(new Microsoft.Practices.Unity.HierarchicalLifetimeManager());// HierachicalLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType(typeof(IGenericService<>),typeof(GenericService<>));
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IChannels, ChannelsService>();
            container.RegisterType<IFees, FeesService>();
            container.RegisterType<IRoutes, RoutesService>();
            container.RegisterType<ISchemes, SchemeService>();
            container.RegisterType<ISourceNode, SourceNodeService>();
            container.RegisterType<ISinkNode, SinkNodeService>();
            container.RegisterType<ITransactionType, TransactionTypeService>();
            container.RegisterType<ITransLogs, TransLogService>();

            // container.Resolve<ApplicationSignInManager>();
            //container.Resolve<ApplicationRoleManager>();
         
            container.RegisterInstance(typeof(IValidationProvider), new ValidationProvider(type => (IValidator)container.Resolve(typeof(Validator<>).MakeGenericType(type))));
            container.RegisterType(typeof(Validator<>), typeof(NullValidator<>));
            return container;
        }
    }
}
