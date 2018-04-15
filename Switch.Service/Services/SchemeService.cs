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
    public class SchemeService:ISchemes
    {
        private readonly IRepository<Schemes> _ModelRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IValidationProvider _ValidationProvider;
        public SchemeService(IRepository<Schemes> _modelRepository, IUnitOfWork unitOfWork, IValidationProvider validationProvider)
        {
            _ModelRepository = _modelRepository;
            _UnitOfWork = unitOfWork;
            _ValidationProvider = validationProvider;
        }
        public async Task<Schemes> GetByIdAsync(Guid id)
        {
            return await _ModelRepository.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Schemes> UpdateAsync(Schemes model)
        {

            // model.IsUpdated = true;
            //   ObjectValidation(model);
            var dbobj = await GetByIdAsync(model.Id);
            if (dbobj == null)
                throw new ProjectException("The Record does not exist in the system");
            dbobj.Description = model.Description;
            dbobj.Fees = model.Fees;
            dbobj.Name = model.Name;
            if(model.Channel!=null)
            {
                dbobj.Channel.Code = model.Channel.Code;
                dbobj.Channel.Description = model.Channel.Description;
                dbobj.Channel.Name = model.Channel.Name;
            }
            if(model.Route!=null)
            {
                dbobj.Route.CardPAN = model.Route.CardPAN;
                dbobj.Route.Description = model.Route.Description;
                dbobj.Route.Name = model.Route.Name;               
            }
            if(model.TransType!=null)
            {
                dbobj.TransType.Code = model.TransType.Code;
                dbobj.TransType.Description = model.TransType.Description;
                dbobj.TransType.Name = model.TransType.Name;
            }

            _ModelRepository.Attach(dbobj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return dbobj;
        }
        public async Task<Schemes> DeleteAsync(Guid Id)
        {
            Schemes obj = await GetByIdAsync(Id);
            if (obj == null)
            {
                throw new ProjectException("The Record does not exist in the system. Thank you");
            }
             obj.IsDeleted = true;
            //  _ValidationProvider.Validate(attendance);
            _ModelRepository.Attach(obj, EntityStatus.Modified);
            await _UnitOfWork.SaveChangesAsync();
            return obj;
        }
        public async Task<Schemes> SaveAsync(Schemes model)
        {
           
            // model.id = SchoolId;
            // ObjectValidation(model);

            Schemes existed = GetQueryable(x => x.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefault();
            if (existed != null)
               // return existed;
               throw new ProjectException("The Scheme already exists.");
            _ModelRepository.Add(model);
            await _UnitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<Schemes>> SaveManyAsync(IEnumerable<Schemes> models)
        {
            _ModelRepository.AddMany(models);
            await _UnitOfWork.SaveChangesAsync();
            return models;
        }
        public IQueryable<Schemes> GetQueryable(Expression<Func<Schemes, bool>> condition, int startIndex = 0, int pageSize = 0)
        {
            Expression<Func<Schemes, bool>> where1 = x => (!x.IsDeleted);
            var where = SwapVisitor.MergeWithAnd<Schemes>(condition, where1);
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
