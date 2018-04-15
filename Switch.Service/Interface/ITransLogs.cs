using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface ITransLogs
    {
        Task<TransLogs> GetByIdAsync(Guid id);
        Task<TransLogs> UpdateAsync(TransLogs model);
        Task<TransLogs> DeleteAsync(Guid Id);
        Task<TransLogs> SaveAsync(TransLogs model);
        Task<IEnumerable<TransLogs>> SaveManyAsync(IEnumerable<TransLogs> model);
        IQueryable<TransLogs> GetQueryable(Expression<Func<TransLogs, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
