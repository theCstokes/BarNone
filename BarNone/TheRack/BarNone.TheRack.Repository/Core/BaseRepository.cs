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
    /// <summary>
    /// Base repository for accessing entities.
    /// </summary>
    /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <seealso cref="BarNone.TheRack.Repository.Core.IRepository{TDomainModel, TDTO}" />
    /// <seealso cref="System.IDisposable" />
    public abstract class BaseRepository<TDomainModel, TDTO> : IRepository<TDomainModel, TDTO>,
        IDisposable
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {
        #region Protected Read Only Field(s).        
        /// <summary>
        /// Domain context.
        /// </summary>
        protected readonly DomainContext context;
        #endregion

        #region Protected Abstract Field(s).        
        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected abstract ConverterResolverDelegate<TDomainModel, TDTO> DataConverter { get; }

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected abstract SetResolverDelegate<TDomainModel> SetResolver { get; }

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected abstract EntityResolverDelegate<TDomainModel> EntityResolver { get; }
        #endregion

        #region Protected Virtual Member(s).
        protected virtual void OnCreate(TDomainModel entity) { }
        protected virtual void OnUpdate(TDomainModel entity) { }
        protected virtual void OnRemove(TDomainModel entity) { } 
        #endregion

        #region Private Field(s).        
        /// <summary>
        /// The database entity set.
        /// </summary>
        private DbSet<TDomainModel> dbSet;
        #endregion

        #region Public Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TDomainModel, TDTO}"/> class.
        /// </summary>
        public BaseRepository()
        {
            context = new DomainContext();
            dbSet = SetResolver(context);
            AllEntites = dbSet;
            Entites = EntityResolver(dbSet);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TDomainModel, TDTO}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepository(DomainContext context)
        {
            this.context = context;
            dbSet = SetResolver(context);
            AllEntites = dbSet;
            Entites = EntityResolver(dbSet);
        }
        #endregion

        #region Public Property(s).        
        /// <summary>
        /// Gets the entites.
        /// </summary>
        /// <value>
        /// The entites.
        /// </value>
        public IQueryable<TDomainModel> Entites { get; }
        public IQueryable<TDomainModel> AllEntites { get; }
        #endregion

        #region Protected Member(s).        
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected TDomainModel Create(TDomainModel model)
        {

            var result = dbSet.Add(model);
            OnCreate(result.Entity);
            return result.Entity;
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected TDomainModel Update(TDomainModel model, List<string> updateFilter)
        {
            var entity = dbSet.Attach(model);
            updateFilter.Select(f => entity.Property(f).IsModified = true);

            OnUpdate(entity.Entity);

            return entity.Entity;
        }

        /// <summary>
        /// Removes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected TDomainModel Remove(TDomainModel model)
        {
            var result = dbSet.Remove(model);
            OnRemove(result.Entity);
            return result.Entity;
        }
        #endregion

        #region Public IDisposable Implementation.        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
        #endregion

        #region Public Abstract IRepository Implementation.        
        /// <summary>
        /// Creates the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public abstract TDomainModel Create(TDTO dto);

        /// <summary>
        /// Gets the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public abstract List<TDomainModel> Get(FilterDTO.WhereFunc where = null);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public abstract TDomainModel Get(int id);

        /// <summary>
        /// Gets the with details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public abstract TDomainModel GetWithDetails(int id);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public abstract TDomainModel Remove(int id);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public abstract TDomainModel Update(int id, TDTO dto);
        #endregion

        #region Public IRepository Implementation.        
        /// <summary>
        /// Gets the entities specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        List<IDomainModel> IRepository.Get(FilterDTO.WhereFunc where)
        {
            return Get(where).Select(v => v as IDomainModel).ToList();
        }

        /// <summary>
        /// Gets the entities specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IDomainModel IRepository.Get(int id)
        {
            return Get(id) as IDomainModel;
        }

        /// <summary>
        /// Gets the entities with details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IDomainModel IRepository.GetWithDetails(int id)
        {
            return GetWithDetails(id) as IDomainModel;
        }

        /// <summary>
        /// Removes the entity specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IDomainModel IRepository.Remove(int id)
        {
            return Remove(id) as IDomainModel;
        }

        /// <summary>
        /// Creates  the entity specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        IDomainModel IRepository.Create(dynamic dto)
        {
            return Create(dto as TDTO) as IDomainModel;
        }

        /// <summary>
        /// Updates entity by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        IDomainModel IRepository.Update(int id, dynamic dto)
        {
            return Update(id, dto as TDTO)  as IDomainModel;
        }
        #endregion
    }
}
