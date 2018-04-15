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
//using Unity.Mvc3;
using Microsoft.Practices.Unity;
using Project.Core.EntityFramework;

namespace Switch.Service.Services
{
    public class ChannelsService:IChannels
    {
        private readonly IRepository<Channels> _ModelRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IValidationProvider _ValidationProvider;
        //public ChannelsService():this( new Repository<Channels>(new SwitchContext() ), new UnitOfWork(), new ValidationProvider())
        //{

        //}
        public ChannelsService(IRepository<Channels> _modelRepository, IUnitOfWork unitOfWork, IValidationProvider validationProvider)
        {
            _ModelRepository = _modelRepository;
            _UnitOfWork = unitOfWork;
            _ValidationProvider = validationProvider;
        }
        public async Task<Channels> GetByIdAsync(Guid id)
        {
            return await _ModelRepository.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Channels> UpdateAsync(Channels model)
        {

            // model.IsUpdated = true;
            //   ObjectValidation(model);
            var dbobj = await GetByIdAsync(model.Id);
            if (dbobj == null)
                throw new ProjectException("The Record does not exist in the system");
            dbobj.Name = model.Name;
            dbobj.Code = model.Code;
            dbobj.Description = model.Description;
            
            _ModelRepository.Attach(dbobj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return dbobj;
        }
        public async Task<Channels> DeleteAsync(Guid Id)
        {
            Channels obj = await GetByIdAsync(Id);
            if (obj == null)
            {
                throw new ProjectException("The record does not exist in the system. Thank you");
            }
             obj.IsDeleted = true;
            
            _ModelRepository.Attach(obj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return obj;
        }
        public async Task<Channels> SaveAsync(Channels model)
        {
            // model.id = SchoolId;
            // ObjectValidation(model);
            
            Channels existed = GetQueryable(x => x.Name.ToLower().Equals(model.Name.ToLower()) || x.Code.ToLower().Equals(model.Code.ToLower())).FirstOrDefault();
            if (existed != null)
               // return existed;
                throw new ProjectException("The Channel already exists.");
            _ModelRepository.Add(model);
            await _UnitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<Channels>> SaveManyAsync(IEnumerable<Channels> models)
        {
            _ModelRepository.AddMany(models);
            await _UnitOfWork.SaveChangesAsync();
            return models;
        }
        public IQueryable<Channels> GetQueryable(Expression<Func<Channels, bool>> condition, int startIndex = 0, int pageSize = 0)
        {
            Expression<Func<Channels, bool>> where1 = x => !x.IsDeleted;
            var where = SwapVisitor.MergeWithAnd<Channels>(condition, where1);
            //  Expression<Func<Country, Client, object>> where = (x, y) => x.Name == y.Name;

            var list = _ModelRepository.GetQueryable(where, x => x.Id, startIndex, pageSize);
            return list;
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
