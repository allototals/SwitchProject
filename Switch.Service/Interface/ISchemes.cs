using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface ISchemes
    {
        Task<Schemes> GetByIdAsync(Guid id);
        Task<Schemes> UpdateAsync(Schemes model);
        Task<Schemes> DeleteAsync(Guid Id);
        Task<Schemes> SaveAsync(Schemes model);
        Task<IEnumerable<Schemes>> SaveManyAsync(IEnumerable<Schemes> channels);
        IQueryable<Schemes> GetQueryable(Expression<Func<Schemes, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
