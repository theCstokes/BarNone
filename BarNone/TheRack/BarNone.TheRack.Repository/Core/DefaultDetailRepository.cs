using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using Microsoft.EntityFrameworkCore;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;
using BarNone.Shared.DataConverter;
using BarNone.TheRack.DataConverters;

namespace BarNone.TheRack.Repository.Core
{
    public abstract class DefaultDetailRepository<TDomainModel, TDTO, TDetailDTO> : BaseRepository<TDomainModel, TDTO>
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {

        #region Public Delegate Definition(s).
        public delegate DbSet<TDomainModel> DbSetResolver(DomainContext context);

        //public delegate IQueryable<TDomainModel> DetailResolver(IQueryable<TDomainModel> set);

        public delegate BaseDetailDataConverter<TDomainModel, TDTO, TDetailDTO, Converters> ConverterResolver();

        public delegate IQueryable<TDomainModel> Resolver(DbSet<TDomainModel> set);
        #endregion

        #region Private Field(s).
        //private DbSetResolver _resolver;
        //private DetailResolver[] _detailResolvers;
        //private ConfigResolver _configResolver;


        protected abstract ConverterResolver DetailDataConverterResolver { get; }
        protected abstract DbSetResolver SetResolver { get; }
        protected abstract Resolver DetailDataResolver { get; }
        #endregion

        #region Public Constructor(s).


        public DefaultDetailRepository() : base(new DomainContext())
        {
        }

        public DefaultDetailRepository(DomainContext context) : base(context)
        {
        }
        #endregion

        public override TDomainModel Create(TDTO dto)
        {

            var dm = DetailDataConverterResolver().CreateDataModel(dto);

            var dataSet = SetResolver(context);

            var createResult = dataSet.Add(dm);

            context.SaveChanges();
            return createResult.Entity;
        }

        public override List<TDomainModel> Get(FilterDTO.WhereFunc where = null)
        {
            var set = SetResolver(context);

            if (where != null)
            {
                return set
                    .Where(b => where(b))
                    .ToList();
            }

            return set.ToList();
        }

        public override TDomainModel Get(int id)
        {
            return SetResolver(context).Where(b => b.ID == id).FirstOrDefault();
        }

        public override TDomainModel GetWithDetails(int id)
        {
            var set = SetResolver(context);

            var q = DetailDataResolver(set);

            return q
                .Where(b => b.ID == id)
                .FirstOrDefault();
        }

        public override TDomainModel Remove(int id)
        {
            var TDomainModel = new TDomainModel
            {
                ID = id
            };

            var result = context.Remove(TDomainModel);
            return result.Entity;
        }

        public override TDomainModel Update(int id, TDTO dto)
        {

            dto.ID = id;

            var dm = DetailDataConverterResolver().CreateDataModel(dto);
            var result = SetResolver(context).Update(dm);

            context.SaveChanges();
            return result.Entity;
        }
    }
}
