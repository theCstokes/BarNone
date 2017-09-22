using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess.Engine;
using TheRack.DomainModel;
using static TheRack.DataAccessAdapter.DataAccessActions;

namespace TheRack.DataAccess
{
    public class DataReadRequest<TDomainModel> : IDataRequest<TDomainModel>
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        
        #region Public Constructor(s).
        public DataReadRequest()
        {
            Type = RequestType.LOAD;
        }
        #endregion

        #region Public Property(s).
        public RequestType Type { get; set; }
        public List<string> Properties { get; set; }
        public Dictionary<string, WriteAction<TDomainModel>> TransformMap { get; set; }
        public EntityMetaData MetaData { get; set; }
        public List<WhereClause> WhereClauses { get; set; }
        #endregion
    }
}
