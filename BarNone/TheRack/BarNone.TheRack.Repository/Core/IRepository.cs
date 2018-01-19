using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

namespace BarNone.TheRack.Repository.Core
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Gets the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        List<IDomainModel> Get(WhereFunc where = null);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IDomainModel Get(int id);

        /// <summary>
        /// Gets the with details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IDomainModel GetWithDetails(int id);

        /// <summary>
        /// Creates the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        IDomainModel Create(dynamic dto);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        IDomainModel Update(int id, dynamic dto);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IDomainModel Remove(int id);
    }

    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository<TDomainModel, TDTO> : IRepository
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {
        /// <summary>
        /// Gets the enetiy specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        new List<TDomainModel> Get(WhereFunc where = null);

        /// <summary>
        /// Gets the entity with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        new TDomainModel Get(int id);

        /// <summary>
        /// Gets the entity with details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        new TDomainModel GetWithDetails(int id);

        /// <summary>
        /// Creates the specified domain model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        TDomainModel Create(TDTO dto);

        /// <summary>
        /// Updates the entity with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        TDomainModel Update(int id, TDTO dto);

        /// <summary>
        /// Removes the entity with specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        new TDomainModel Remove(int id);
    }
}
