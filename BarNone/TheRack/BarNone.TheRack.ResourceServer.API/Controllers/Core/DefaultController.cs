using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Core
{
    /// <summary>
    /// Generic detail controller implementation.
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
    /// <typeparam name="TRepo">The type of the repo.</typeparam>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DetailController{TDTO}" />
    public class DefaultController<TDTO, TDomainModel, TRepo> : BasicController<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
        where TRepo : BaseRepository<TDomainModel, TDTO>
    {

        #region Public Delegate Definition(s).        
        /// <summary>
        /// Builder delegate to create an entity repository.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public delegate TRepo RepoBuilder(DomainContext context);
        #endregion

        #region Private Field(s).
        private RepoBuilder _builder;
        #endregion

        #region Public Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDetailController{TDTO, TDomainModel, TRepo}"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public DefaultController(RepoBuilder builder)
        {
            _builder = builder;
        }
        #endregion

        #region Public DetailController Implementation.        
        /// <summary>
        /// Deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override IActionResult Delete(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = _builder(context))
                {
                    var result = repo.Remove(id);
                    context.SaveChanges();
                    return EntityResponse.Response(result);
                }
            }
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        public override IActionResult GetAll()
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = _builder(context))
                {
                    var filter = FilterRequest;
                    if (filter != null)
                    {
                        return EntityResponse.Response(repo.Get(filter.GetWhere()));
                    }
                    return EntityResponse.Response(repo.Get());
                }
            }
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override IActionResult GetByID(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = _builder(context))
                {
                    return EntityResponse.Response(repo.Get(id));
                }
            }
        }

        /// <summary>
        /// Creates the entity from the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override IActionResult Post([FromBody]TDTO dto)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = _builder(context))
                {
                    var result = repo.Create(dto);
                    context.SaveChanges();
                    return EntityResponse.DetailResponse(result);
                }
            }
        }

        /// <summary>
        /// Updates the entity from specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override IActionResult Put(int id, [FromBody] TDTO dto)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = _builder(context))
                {
                    var result = repo.Update(id, dto);
                    context.SaveChanges();
                    return EntityResponse.Response(result);
                }
            }
        } 
        #endregion
    }
}
