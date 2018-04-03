using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.DataAccess;
using Microsoft.EntityFrameworkCore;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverters;

namespace BarNone.TheRack.Repository.Core
{
    /// <summary>
    /// Default deatil controller.
    /// </summary>
    /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <typeparam name="TDetailDTO">The type of the detail dto.</typeparam>
    /// <seealso cref="BarNone.TheRack.Repository.Core.BaseRepository{TDomainModel, TDTO}" />
    public abstract class DefaultDetailRepository<TDomainModel, TDTO, TDetailDTO> : BaseRepository<TDomainModel, TDTO>
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {
        #region Private Field(s).        
        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected abstract DetailResolverDelegate<TDomainModel> DetailEntityResolver { get; }
        #endregion

        #region Public Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDetailRepository{TDomainModel, TDTO, TDetailDTO}"/> class.
        /// </summary>
        public DefaultDetailRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDetailRepository{TDomainModel, TDTO, TDetailDTO}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DefaultDetailRepository(DomainContext context) : base(context)
        {
            //Converter = DetailDataConverterResolver();
        }
        #endregion

        /// <summary>
        /// Creates the specified domain model from the dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override TDomainModel Create(TDTO dto)
        {

            var dm = DataConverter(dto);

            var result = Create(dm);

            return result;
        }

        /// <summary>
        /// Gets entities using the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public override List<TDomainModel> Get(FilterDTO.WhereFunc where = null)
        {
            if (where != null)
            {
                return Entites
                    .Where(b => where(b))
                    .ToList();
            }

            return Entites.ToList();
        }

        /// <summary>
        /// Gets entities using the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TDomainModel Get(int id)
        {
            return Entites.Where(b => b.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets entities the with details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TDomainModel GetWithDetails(int id)
        {
            var detailEntities = DetailEntityResolver(Entites);

            var items = detailEntities.ToList();

            //var r = items.First(d => d.ID == id);

            var result = items
                .Where(b => b.ID == id)
                .FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Removes the entity with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TDomainModel Remove(int id)
        {
            var TDomainModel = new TDomainModel
            {
                ID = id
            };

            var result = Remove(TDomainModel);
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

            dto.ID = id;

            var dm = DataConverter(dto);
            var result = Update(dm, dto.UpdateFilter);

            //context.SaveChanges();
            return result;
        }
    }
}
