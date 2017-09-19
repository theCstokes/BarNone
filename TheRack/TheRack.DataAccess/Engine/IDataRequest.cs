using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DomainModel;
using static TheRack.DataAccessAdapter.DataAccessActions;

namespace TheRack.DataAccess.Engine
{
    public interface IDataRequest<TDomainModel>
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        RequestType Type { get; set; }
        EntityMetaData MetaData { get; set; }
        List<string> Properties { get; set; }
        Dictionary<string, WriteAction<TDomainModel>> TransformMap { get; set; }
        List<WhereClause> WhereClauses { get; set; }
    }
}
