using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switch.Service.Interface;
using Project.Core.Data;
using Project.Core.Validation;
using Switch.Data.Models;
using System.Linq.Expressions;
using Project.Core.Exceptions;

namespace Switch.Service.Services
{
    public class TransactionTypeService:ITransactionType
    {
        private readonly IRepository<TransactionType> _ModelRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IValidationProvider _ValidationProvider;
        public TransactionTypeService(IRepository<TransactionType> _modelRepository, IUnitOfWork unitOfWork, IValidationProvider validationProvider)
        {
            _ModelRepository = _modelRepository;
            _UnitOfWork = unitOfWork;
            _ValidationProvider = validationProvider;
        }
        public async Task<TransactionType> GetByIdAsync(Guid id)
        {
            return await _ModelRepository.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<TransactionType> UpdateAsync(TransactionType model)
        {

            // model.IsUpdated = true;
            //   ObjectValidation(model);
            var dbobj = await GetByIdAsync(model.Id);
            if (dbobj == null)
                throw new ProjectException("The Record does not exist in the system");
            dbobj.Code = model.Code;
            dbobj.Name = model.Name;
            dbobj.Description = model.Description;

            _ModelRepository.Attach(dbobj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return dbobj;
        }
        public async Task<TransactionType> DeleteAsync(Guid Id)
        {
            TransactionType obj = await GetByIdAsync(Id);
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
        public async Task<TransactionType> SaveAsync(TransactionType model)
        {

            // model.id = SchoolId;
            // ObjectValidation(model);

            TransactionType existed = GetQueryable(x => x.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefault();
            if (existed != null)
                return existed;
               // throw new ProjectException("The Route already exists.");
            _ModelRepository.Add(model);
            await _UnitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<TransactionType>> SaveManyAsync(IEnumerable<TransactionType> models)
        {
            _ModelRepository.AddMany(models);
            await _UnitOfWork.SaveChangesAsync();
            return models;
        }
        public IQueryable<TransactionType> GetQueryable(Expression<Func<TransactionType, bool>> condition, int startIndex = 0, int pageSize = 0)
        {
            Expression<Func<TransactionType, bool>> where1 = x => (!x.IsDeleted);
            var where = SwapVisitor.MergeWithAnd<TransactionType>(condition, where1);
            //  Expression<Func<Country, Client, object>> where = (x, y) => x.Name == y.Name;

            return _ModelRepository.GetQueryable(where, x => x.Id, startIndex, pageSize);
        }
        
        public void Dispose()
        {
            if (_ModelRepository != null)
                _ModelRepository.Dispose();

            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }
    }
}
