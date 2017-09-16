using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.Repository
{
    public abstract class BaseRepository<TDTO, TDomainModel>
    {
        //public delegate TDTO MapToDTO(TDomainModel model);
        //public delegate TDomainModel MapToDomainModel(TDTO dto);

        //public abstract MapToDTO ToDTO { get; }
        //public abstract MapToDomainModel ToDomainModel { get; }
    }
}
