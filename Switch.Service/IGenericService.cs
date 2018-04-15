using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service
{
    public interface IGenericService<T> where T:class
    {
        bool Create(T item);
        bool Update(T item);
        IList<T> Pagin(int pageNo, int pageSize);
        T GetById(long id);
        void Delete(int Id);
        T FindBy(Expression<Func<T, bool>> expression);
        List<T> FilterBy(Expression<Func<T, bool>> expression);
        IList<T> SelectAll();
    }
}
