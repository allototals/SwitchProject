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
    public class RoutesService:IRoutes
    {
        private readonly IRepository<Routes> _ModelRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IValidationProvider _ValidationProvider;
        public RoutesService(IRepository<Routes> _modelRepository, IUnitOfWork unitOfWork, IValidationProvider validationProvider)
        {
            _ModelRepository = _modelRepository;
            _UnitOfWork = unitOfWork;
            _ValidationProvider = validationProvider;
            
        }
        public async Task<Routes> GetByIdAsync(Guid id)
        {
            return await _ModelRepository.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Routes> UpdateAsync(Routes model)
        {

            // model.IsUpdated = true;
            //   ObjectValidation(model);
            var dbobj = await GetByIdAsync(model.Id);
            if (dbobj == null)
                throw new ProjectException("The Record does not exist in the system");
            dbobj.CardPAN = model.CardPAN;
            dbobj.Description = model.Description;
            dbobj.Name = model.Name;
            if(model.SinkNode!=null)
            {
                dbobj.SinkNode.HostName = model.SinkNode.HostName;
                dbobj.SinkNode.IPAdress = model.SinkNode.IPAdress;
                dbobj.SinkNode.Name = model.SinkNode.Name;
                dbobj.SinkNode.Port = model.SinkNode.Port;
                
            }

            _ModelRepository.Attach(dbobj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return dbobj;
        }
        public async Task<Routes> DeleteAsync(Guid Id)
        {
            Routes obj = await GetByIdAsync(Id);
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
        public async Task<Routes> SaveAsync(Routes model)
        {

            // model.id = SchoolId;
            // ObjectValidation(model);

            Routes existed = GetQueryable(x => x.Name.ToLower().Equals(model.Name.ToLower()) ).FirstOrDefault();
            if (existed != null)
                return existed;
               // throw new ProjectException("The Route already exists.");
            _ModelRepository.Add(model);
            await _UnitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<Routes>> SaveManyAsync(IEnumerable<Routes> models)
        {
            _ModelRepository.AddMany(models);
            await _UnitOfWork.SaveChangesAsync();
            return models;
        }
        public IQueryable<Routes> GetQueryable(Expression<Func<Routes, bool>> condition, int startIndex = 0, int pageSize = 0)
        {
            Expression<Func<Routes, bool>> where1 = x => !x.IsDeleted;
            var where = SwapVisitor.MergeWithAnd<Routes>(condition, where1);
            //Expression<Func<Country, bool>> where1 = x => (!);
            //var where = SwapVisitor.MergeWithAnd<Country>(condition, where1);
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
