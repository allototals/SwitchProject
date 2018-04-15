using Switch.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transaction;
using Switch.Data.Concrete;
using System.Linq.Expressions;
using NHibernate.Linq;
using StructureMap;
using StructureMap.Configuration.DSL;
using System.Configuration;


namespace Switch.Service
{
    public class GenericService<T>:Registry,IGenericService<T> where T:Switch.Core.Model.Entity
    {
           
        //AppSettings["NodePort"]
        // string conn = connect; //@"Data Source=.\SQL2012; Database=Switch;Password=pa55w0rd; User Id =sa";
        private readonly IInterface _sessionManager= new SessionManager<T>();
        public GenericService()
        {
            For(typeof(IInterface)).Use(typeof(SessionManager<>));

        }
        public GenericService(IInterface sessionManager)
        {
            _sessionManager = sessionManager;
        }
        public  bool Create(T item)
        {
            using (ISession session = _sessionManager.Session)//OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                   

                    session.SaveOrUpdate(item);
                    transaction.Commit();
                    return true;
                }
            }
        }

        public  bool Update(T item)
        {
           
           
            using (ISession session = _sessionManager.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        if (item == null)
                            throw new ArgumentNullException("This error is unknown!");
                        session.Merge(item);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {

                    }
                    return true;
                }


            }
        }
        public  IList<T> SelectAll()
        {
            ISession session = _sessionManager.Session;

            using (ITransaction transaction = session.BeginTransaction())
            {
                IList<T> list = session.CreateCriteria<T>().List<T>();

                return list;
                // session.Close();
            }


        }

        public  IList<T> Pagin(int pageNo, int pageSize)
        {
            ISession session = _sessionManager.Session;

            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.Query<T>().Skip(pageNo * pageSize).Take(pageSize).ToList();
            }
        }

        public  T GetById(long id)
        {
            using (ISession session = _sessionManager.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    return session.Get<T>(id);
                }
            }
        }
        public  void Delete(int Id)
        {
            using (ISession session = _sessionManager.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var del = session.Load<T>(Id);
                    session.Delete(del);
                    session.Flush();
                    transaction.Commit();
                    // return true;
                }

            }
        }

        public  IQueryable<T> All()
        {
            using (ISession session = _sessionManager.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    return session.Query<T>();
                }
            }
        }
        public  T FindBy(Expression<Func<T, bool>> expression)
        {
            using (ISession session = _sessionManager.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    return session.Query<T>().Where(expression).SingleOrDefault();
                }
            }
        }

        public  List<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            ISession session = _sessionManager.Session;
            //{
            IList<T>  result =null;
            using (ITransaction transaction = session.BeginTransaction())
            {

                 result = session.Query<T>().Where(expression).ToList();// ToList();
                
            }
            var temp = result.ToList();
            return result.ToList();
            //}
        }
    }
}
