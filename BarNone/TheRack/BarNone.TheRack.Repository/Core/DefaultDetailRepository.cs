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
    public abstract class DefaultDetailRepository<TDomainModel, TDTO, TDetailDTO> : BaseRepository<TDomainModel, TDTO>
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {
        #region Private Field(s).
        protected abstract DetailResolverDelegate<TDomainModel> DetailEntityResolver { get; }
        #endregion

        #region Public Constructor(s).


        public DefaultDetailRepository() : base()
        {
        }

        public DefaultDetailRepository(DomainContext context) : base(context)
        {
            //Converter = DetailDataConverterResolver();
        }
        #endregion

        //protected BaseDetailDataConverter<TDomainModel, TDTO, TDetailDTO, Converters> Converter { get; private set; }

        public override TDomainModel Create(TDTO dto)
        {

            var dm = DataConverter(dto);

            var result = Create(dm);

            return result;
        }

        public override List<TDomainModel> Get(FilterDTO.WhereFunc where = null)
        {
            if (where != null)
            {
                return entites
                    .Where(b => where(b))
                    //.Where(b => b.ID == context.)
                    .ToList();
            }

            return entites.ToList();
        }

        public override TDomainModel Get(int id)
        {
            return entites.Where(b => b.ID == id).FirstOrDefault();
        }

        public override TDomainModel GetWithDetails(int id)
        {
            var detailEntities = DetailEntityResolver(entites);

            return detailEntities
                .Where(b => b.ID == id)
                .FirstOrDefault();
        }

        public override TDomainModel Remove(int id)
        {
            var TDomainModel = new TDomainModel
            {
                ID = id
            };

            var result = Remove(TDomainModel);
            return result;
        }

        public override TDomainModel Update(int id, TDTO dto)
        {

            dto.ID = id;

            var dm = DataConverter(dto);
            var result = Update(dm);

            //context.SaveChanges();
            return result;
        }

        //private IQueryable<TDomainModel> Load()
        //{

        //    var set = SetResolver(context);
        //    if(LoadFliter != null)
        //    {
        //        return LoadFliter(set);
        //    }
        //    return set;
        //}
    }
}
