using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface ICRUD<T> where T: class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T model);
        Task<T> DeleteAsync(Guid Id);
        Task<T> SaveAsync(T model);
        Task<IEnumerable<T>> SaveManyAsync(IEnumerable<T> models);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
