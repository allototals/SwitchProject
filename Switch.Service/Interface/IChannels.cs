using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Interface
{
    public interface IChannels
    {
        Task<Channels> GetByIdAsync(Guid id);
        Task<Channels> UpdateAsync(Channels model);
        Task<Channels> DeleteAsync(Guid Id);
        Task<Channels> SaveAsync(Channels model);
        Task<IEnumerable<Channels>> SaveManyAsync(IEnumerable<Channels> channels);
        IQueryable<Channels> GetQueryable(Expression<Func<Channels, bool>> condition, int startIndex = 0, int pageSize = 0);
    }
}
