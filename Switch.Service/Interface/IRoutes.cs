using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface IRoutes
    {
        Task<Routes> GetByIdAsync(Guid id);
        Task<Routes> UpdateAsync(Routes model);
        Task<Routes> DeleteAsync(Guid Id);
        Task<Routes> SaveAsync(Routes model);
        Task<IEnumerable<Routes>> SaveManyAsync(IEnumerable<Routes> models);
        IQueryable<Routes> GetQueryable(Expression<Func<Routes, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
