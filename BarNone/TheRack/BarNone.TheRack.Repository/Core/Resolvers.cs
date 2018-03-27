using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.Repository.Core
{
    public static class Resolvers
    {
        /// <summary>
        /// Delegate to resolve type converter.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <typeparam name="TDTO">The type of the dto.</typeparam>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public delegate TDomainModel ConverterResolverDelegate<TDomainModel, TDTO>(TDTO dto)
            where TDomainModel : ITrackable<TDomainModel>, new()
            where TDTO : ITrackableDTO<TDTO>, new();

        /// <summary>
        /// Delegate to resolve set.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public delegate DbSet<TDomainModel> SetResolverDelegate<TDomainModel>(DomainContext context)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();

        /// <summary>
        /// Delegate to resolve entities.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        public delegate IQueryable<TDomainModel> EntityResolverDelegate<TDomainModel>(DbSet<TDomainModel> set)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();

        public delegate IQueryable<TDomainModel> ListFormattingDelegate<TDomainModel>(DbSet<TDomainModel> set)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();

        /// <summary>
        /// Delegate to resolve detail entities.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        public delegate IQueryable<TDomainModel> DetailResolverDelegate<TDomainModel>(IQueryable<TDomainModel> set)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();
    }
}
