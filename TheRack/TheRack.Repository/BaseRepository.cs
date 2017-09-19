using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess;
using TheRack.DataAccessAdapter;
using TheRack.DomainModel;

namespace TheRack.Repository
{
    public abstract class BaseRepository<TDTO, TDomainModel> 
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        //public delegate TDTO MapToDTO(TDomainModel model);
        //public delegate TDomainModel MapToDomainModel(TDTO dto);

        //public abstract MapToDTO ToDTO { get; }
        //public abstract MapToDomainModel ToDomainModel { get; }

        public List<TDomainModel> CreateEntities(BaseAdapter<TDTO, TDomainModel> adapter, List<SQLResults> resultList)
        {
            return resultList.Select(result =>
            {
                var model = new TDomainModel();

                foreach (var pair in result)
                {
                    adapter.FullWriteMap[pair.Key](model, pair.Value);
                }
                return model;
            }).ToList();
        }
    }
}
