using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface IFees
    {
        Task<Fees> GetByIdAsync(Guid id);
        Task<Fees> UpdateAsync(Fees model);
        Task<Fees> DeleteAsync(Guid Id);
        Task<Fees> SaveAsync(Fees model);
        Task<IEnumerable<Fees>> SaveManyAsync(IEnumerable<Fees> models);
        IQueryable<Fees> GetQueryable(Expression<Func<Fees, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
