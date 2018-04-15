using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface  ISinkNode
    {
        Task<SinkNode> GetByIdAsync(Guid id);
        Task<SinkNode> UpdateAsync(SinkNode model);
        Task<SinkNode> DeleteAsync(Guid Id);
        Task<SinkNode> SaveAsync(SinkNode model);
        Task<IEnumerable<SinkNode>> SaveManyAsync(IEnumerable<SinkNode> model);
        IQueryable<SinkNode> GetQueryable(Expression<Func<SinkNode, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
