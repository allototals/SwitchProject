using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Switch.Service.Interface
{
    public interface ITransactionType
    {
        Task<TransactionType> GetByIdAsync(Guid id);
        Task<TransactionType> UpdateAsync(TransactionType model);
        Task<TransactionType> DeleteAsync(Guid Id);
        Task<TransactionType> SaveAsync(TransactionType model);
        Task<IEnumerable<TransactionType>> SaveManyAsync(IEnumerable<TransactionType> model);
        IQueryable<TransactionType> GetQueryable(Expression<Func<TransactionType, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
