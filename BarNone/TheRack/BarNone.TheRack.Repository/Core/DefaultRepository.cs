using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository.Core
{
    /// <summary>
    /// Default repository for generic entity.
    /// </summary>
    /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <seealso cref="BarNone.TheRack.Repository.Core.BaseRepository{TDomainModel, TDTO}" />
    public abstract class DefaultRepository<TDomainModel, TDTO> : BaseRepository<TDomainModel, TDTO>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRepository{TDomainModel, TDTO}"/> class.
        /// </summary>
        public DefaultRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRepository{TDomainModel, TDTO}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DefaultRepository(DomainContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the entities with a specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public override List<TDomainModel> Get(WhereFunc where = null)
        {
            if (where != null)
            {
                return entites.Where((u) => where(u))
                    .ToList();
            }
            return entites.ToList();
        }

        /// <summary>
        /// Gets the entities with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TDomainModel Get(int id)
        {
            var result = entites.Where(c => c.ID == id).First();

            return result;
        }

        /// <summary>
        /// Gets entity with details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TDomainModel GetWithDetails(int id)
        {
            var result = entites.Where(c => c.ID == id).First();

            return result;
        }

        /// <summary>
        /// Creates the specified domain model from the dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override TDomainModel Create(TDTO dto)
        {
            var entity = DataConverter(dto);
            var result = Create(entity);

            return result;
        }

        /// <summary>
        /// Updates the entity with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override TDomainModel Update(int id, TDTO dto)
        {
            var entity = DataConverter(dto);
            entity.ID = id;
            var result = Update(entity, dto.UpdateFilter);

            return result;
        }

        /// <summary>
        /// Removes the entity with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TDomainModel Remove(int id)
        {
            var result = Remove(new TDomainModel
            {
                ID = id
            });

            return result;
        }
    }
}
