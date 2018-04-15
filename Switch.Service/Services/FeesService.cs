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
    public class FeesService:IFees
    {
         private readonly IRepository<Fees> _ModelRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IValidationProvider _ValidationProvider;
        public FeesService(IRepository<Fees> _modelRepository, IUnitOfWork unitOfWork, IValidationProvider validationProvider)
        {
            _ModelRepository = _modelRepository;
            _UnitOfWork = unitOfWork;
            _ValidationProvider = validationProvider;
        }
        public async Task<Fees> GetByIdAsync(Guid id)
        {
            return await _ModelRepository.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Fees> UpdateAsync(Fees model)
        {

            // model.IsUpdated = true;
            //   ObjectValidation(model);
            var dbobj = await GetByIdAsync(model.Id);
            if (dbobj == null)
                throw new ProjectException("The Record does not exist in the system");
            dbobj.Name = model.Name;
            dbobj.FlatAmount = model.FlatAmount;
            dbobj.Maximum = model.Maximum;
            dbobj.Minimum = model.Minimum;
            dbobj.Percent = model.Percent;
            
            _ModelRepository.Attach(dbobj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return dbobj;
        }
        public async Task<Fees> DeleteAsync(Guid Id)
        {
            Fees obj = await GetByIdAsync(Id);
            if (obj == null)
            {
                throw new ProjectException("The record does not exist in the system. Thank you");
            }
            // Country.IsDeleted = true;
            //  _ValidationProvider.Validate(attendance);
            _ModelRepository.Attach(obj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return obj;
        }
        public async Task<Fees> SaveAsync(Fees model)
        {

            // model.id = SchoolId;
            // ObjectValidation(model);
            
            Fees existed = GetQueryable(x => x.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefault();
            if (existed != null)
                return existed;
               // throw new ProjectException("The Movie already exists.");
            _ModelRepository.Add(model);
            await _UnitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<Fees>> SaveManyAsync(IEnumerable<Fees> models)
        {
            _ModelRepository.AddMany(models);
            await _UnitOfWork.SaveChangesAsync();
            return models;
        }
        public IQueryable<Fees> GetQueryable(Expression<Func<Fees, bool>> condition, int startIndex = 0, int pageSize = 0)
        {
            Expression<Func<Fees, bool>> where1 = x => (!x.IsDeleted);
            var where = SwapVisitor.MergeWithAnd<Fees>(condition, where1);
            //  Expression<Func<Country, Client, object>> where = (x, y) => x.Name == y.Name;

            return _ModelRepository.GetQueryable(condition, x => x.Id, startIndex, pageSize);
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
