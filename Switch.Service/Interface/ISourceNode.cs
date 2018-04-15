using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface ISourceNode
    {
        Task<SourceNode> GetByIdAsync(Guid id);
        Task<SourceNode> UpdateAsync(SourceNode model);
        Task<SourceNode> DeleteAsync(Guid Id);
        Task<SourceNode> SaveAsync(SourceNode model);
        Task<IEnumerable<SourceNode>> SaveManyAsync(IEnumerable<SourceNode> model);
        IQueryable<SourceNode> GetQueryable(Expression<Func<SourceNode, bool>> condition, int startIndex = 0, int pageSize = 0);
        
    }
}
