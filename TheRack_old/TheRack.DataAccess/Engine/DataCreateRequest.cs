using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess.Engine;
using TheRack.DomainModel;
using static TheRack.DataAccessAdapter.DataAccessActions;

namespace TheRack.DataAccess
{
    public class DataCreateRequest<TDomainModel> : IDataRequest<TDomainModel>
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        #region Public Constructor(s).
        
        public DataCreateRequest(RequestType type)
        {
            Type = type;
        }
        #endregion

        #region Public Property(s).
        public RequestType Type { get; set; }
        public DataEntity Entity { get; set; }
        public List<UpdateRequest> UpdateFilter { get; set; }
        public Dictionary<string, WriteAction<TDomainModel>> TransformMap { get; set; }
        public EntityMetaData MetaData { get; set; }
        public List<WhereClause> WhereClauses { get; set; }

        public List<string> Properties { get; set; }
        #endregion
    }
}
