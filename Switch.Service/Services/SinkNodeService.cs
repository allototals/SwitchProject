using Project.Core.Data;
using Project.Core.Exceptions;
using Project.Core.Validation;
using Switch.Data.Models;
using Switch.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service.Services
{
    public class SinkNodeService:ISinkNode
    {
        private readonly IRepository<SinkNode> _ModelRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IValidationProvider _ValidationProvider;
        public SinkNodeService(IRepository<SinkNode> _modelRepository, IUnitOfWork unitOfWork, IValidationProvider validationProvider)
        {
            _ModelRepository = _modelRepository;
            _UnitOfWork = unitOfWork;
            _ValidationProvider = validationProvider;
        }
        public async Task<SinkNode> GetByIdAsync(Guid id)
        {
            return await _ModelRepository.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<SinkNode> UpdateAsync(SinkNode model)
        {

            // model.IsUpdated = true;
            //   ObjectValidation(model);
            var dbobj = await GetByIdAsync(model.Id);
            if (dbobj == null)
                throw new ProjectException("The Record does not exist in the system");

            dbobj.HostName = model.HostName;
            dbobj.IPAdress = model.IPAdress;
            dbobj.Name = model.Name;
            dbobj.Port = model.Port;
            dbobj.Status = model.Status;

            _ModelRepository.Attach(dbobj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return dbobj;
        }
        public async Task<SinkNode> DeleteAsync(Guid Id)
        {
            SinkNode obj = await GetByIdAsync(Id);
            if (obj == null)
            {
                throw new ProjectException("The record does not exist in the system. Thank you");
            }
            obj.IsDeleted = true;
            //  _ValidationProvider.Validate(attendance);
            _ModelRepository.Attach(obj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return obj;
        }
        public async Task<SinkNode> SaveAsync(SinkNode model)
        {

            // model.id = SchoolId;
            // ObjectValidation(model);

            var existed = GetQueryable(x => x.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefault();
            if (existed != null)
            //    return existed;
                throw new ProjectException("The Sink Node already exists.");
            _ModelRepository.Add(model);
            await _UnitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<SinkNode>> SaveManyAsync(IEnumerable<SinkNode> models)
        {
            _ModelRepository.AddMany(models);
            await _UnitOfWork.SaveChangesAsync();
            return models;
        }
        public IQueryable<SinkNode> GetQueryable(Expression<Func<SinkNode, bool>> condition, int startIndex = 0, int pageSize = 0)
        {
            Expression<Func<SinkNode, bool>> where1 = x => (!x.IsDeleted);
            var where = SwapVisitor.MergeWithAnd<SinkNode>(condition, where1);
            //  Expression<Func<Country, Client, object>> where = (x, y) => x.Name == y.Name;

            return _ModelRepository.GetQueryable(where, x => x.Id, startIndex, pageSize);
        }
    }
}
