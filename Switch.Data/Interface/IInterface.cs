using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Interface
{
    public interface IInterface
    {
        ISession Session { get; }
        void Dispose();
        void CleanUp();
        void Rollback();
        void Commit();
    }
}
