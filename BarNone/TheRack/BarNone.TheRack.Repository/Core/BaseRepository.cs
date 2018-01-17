using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository.Core
{
    public abstract class BaseRepository<TDomainModel, TDTO> : IRepository<TDomainModel, TDTO>,
        IDisposable
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {        
        #region Protected Read Only Field(s).
        protected readonly DomainContext context;

        protected abstract ConverterResolverDelegate<TDomainModel, TDTO> DataConverter { get; }
        protected abstract SetResolverDelegate<TDomainModel> SetResolver { get; }
        protected abstract EntityResolverDelegate<TDomainModel> EntityResolver { get; }
        #endregion

        #region Private Field(s).
        private DbSet<TDomainModel> dbSet;
        #endregion

        #region Public Constructor(s).
        public BaseRepository()
        {
            context = new DomainContext();
            //Config = new ConvertConfig();

            dbSet = SetResolver(context);
            entites = EntityResolver(dbSet);
        }

        public BaseRepository(DomainContext context)
        {
            this.context = context;
            //Config = new ConvertConfig();

            dbSet = SetResolver(context);
            entites = EntityResolver(dbSet);
        }
        #endregion

        #region Public Property(s).
        //public ConvertConfig Config { get; set; }

        public IQueryable<TDomainModel> entites { get; }
        #endregion

        #region Protected Member(s).

        protected TDomainModel Create(TDomainModel model)
        {

            return dbSet.Add(model).Entity;
        }

        protected TDomainModel Update(TDomainModel model)
        {
            return dbSet.Update(model).Entity;
        }

        protected TDomainModel Remove(TDomainModel model)
        {
            return dbSet.Remove(model).Entity;
        }
        #endregion

        #region Public IDisposable Implementation.
        public void Dispose()
        {
            context.Dispose();
        } 
        #endregion

        #region Public Abstract IRepository Implementation.
        public abstract TDomainModel Create(TDTO dto);
        public abstract List<TDomainModel> Get(FilterDTO.WhereFunc where = null);
        public abstract TDomainModel Get(int id);
        public abstract TDomainModel GetWithDetails(int id);
        public abstract TDomainModel Remove(int id);
        public abstract TDomainModel Update(int id, TDTO dto);
        #endregion

        #region Public IRepository Implementation.
        List<IDomainModel> IRepository.Get(FilterDTO.WhereFunc where)
        {
            return Get(where).Select(v => v as IDomainModel).ToList();
        }

        IDomainModel IRepository.Get(int id)
        {
            return Get(id) as IDomainModel;
        }

        IDomainModel IRepository.GetWithDetails(int id)
        {
            return GetWithDetails(id) as IDomainModel;
        }

        IDomainModel IRepository.Remove(int id)
        {
            return Remove(id) as IDomainModel;
        }

        IDomainModel IRepository.Create(dynamic dto)
        {
            return Create(dto as TDTO) as IDomainModel;
        }

        IDomainModel IRepository.Update(int id, dynamic dto)
        {
            return Update(id, dto as TDTO)  as IDomainModel;
        }
        #endregion
    }
}
