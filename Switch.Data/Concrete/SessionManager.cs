using NHibernate;
using NHibernate.Context;
using Switch.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using System.Configuration;
using Switch.Core.Model;

namespace Switch.Data.Concrete
{
    
    public class SessionManager<T> : IInterface, IDisposable where T : class
    {

        private readonly ISessionFactory _sessionFactory;
       private  string connect = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;

        public ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        public ISession Session
        {
            get
            {
                if(!CurrentSessionContext.HasBind(SessionFactory))
                {
                    CurrentSessionContext.Bind(SessionFactory.OpenSession());
                }
                return _sessionFactory.OpenSession();
                //if (!ManagedWebSessionContext.HasBind(HttpContext.Current, SessionFactory))
                //{
                //    ManagedWebSessionContext.Bind(HttpContext.Current, SessionFactory.OpenSession());
                //}
                //return _sessionFactory.GetCurrentSession();
            }
            set 
            { 
                ManagedWebSessionContext.Bind(HttpContext.Current, SessionFactory.OpenSession());
            }
        }

        private readonly ITransaction _transaction;

        public SessionManager()
        {
            if (_sessionFactory == null)
            {
                //IDictionary<string, string> props = new Dictionary<string, string>();
                //props.Add("current_session_context_class", "web");
                //props.Add("show_sql", "true");
                log4net.Config.XmlConfigurator.Configure();
                //log4net.Config.XmlConfigurator.Configure();
                var asss = Assembly.Load("Switch.Core");
                _sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connect))
                    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load("Switch.Core")))
                   // .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Entity>())
                    .ExposeConfiguration(m => m.SetProperty("current_session_context_class", "web"))
                    .ExposeConfiguration(m => m.SetProperty("show_sql","true"))
                        .ExposeConfiguration(m => new NHibernate.Tool.hbm2ddl.SchemaUpdate(m)
                            //.Mappings(m => m.FluentMappings.AddFromAssemblyOf<TransactionLogMap>()).ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg)
                .Execute(false, true)).BuildSessionFactory();
            }
        }



        public void Rollback()
        {
            if (_transaction.IsActive)
                _transaction.Rollback();
        }


        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }


        public void Commit()
        {
            if (_transaction.IsActive)
                _transaction.Commit();
        }

        /// <summary>
        /// Clean up the session.
        /// </summary>
        public void CleanUp()
        {
            CleanUp(HttpContext.Current, _sessionFactory);
        }

        public static void CleanUp(HttpContext context, ISessionFactory sessionFactory)
        {
            ISession session = ManagedWebSessionContext.Unbind(context, sessionFactory);

            if (session != null)
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    session.Transaction.Rollback();
                }
                else if (context.Error == null && session.IsDirty())
                {
                    session.Flush();
                }
                session.Close();
            }
        }

        public void Dispose()
        {
            CleanUp();
            _sessionFactory.Dispose();
        }

    }
}
