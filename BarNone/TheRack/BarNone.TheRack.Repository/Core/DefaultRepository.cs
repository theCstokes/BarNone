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
    public abstract class DefaultRepository<TDomainModel, TDTO> : BaseRepository<TDomainModel, TDTO>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {

        //protected BaseDataConverter<TDomainModel, TDTO, Converters> Converter { get; private set; }

        public DefaultRepository() : base()
        {
        }

        public DefaultRepository(DomainContext context) : base(context)
        {
        }

        public override List<TDomainModel> Get(WhereFunc where = null)
        {
            if (where != null)
            {
                return entites.Where((u) => where(u))
                    .ToList();
            }
            return entites.ToList();
        }

        public override TDomainModel Get(int id)
        {
            var result = entites.Where(c => c.ID == id).First();

            //context.SaveChanges();

            return result;
        }

        public override TDomainModel GetWithDetails(int id)
        {
            var result = entites.Where(c => c.ID == id).First();

            //context.SaveChanges();

            return result;
        }

        public override TDomainModel Create(TDTO dto)
        {
            var entity = DataConverter(dto);
            var result = Create(entity);

            //context.SaveChanges();

            return result;
        }

        public override TDomainModel Update(int id, TDTO dto)
        {
            var entity = DataConverter(dto);
            entity.ID = id;
            var result = Update(entity);

            //context.SaveChanges();

            return result;
        }

        public override TDomainModel Remove(int id)
        {
            var result = Remove(new TDomainModel
            {
                ID = id
            });

            //context.SaveChanges();

            return result;
        }
    }
}
