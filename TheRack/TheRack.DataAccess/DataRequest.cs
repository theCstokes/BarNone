using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class DataRequest
    {
        #region Public Constructor(s).
        public DataRequest()
        {

        }

        public DataRequest(RequestType type, DataEntity entity, List<UpdateRequest> updateFilter = null)
        {
            Type = type;
            Entity = entity;
            UpdateFilter = updateFilter;
        }
        #endregion

        #region Public Property(s).
        public RequestType Type { get; set; }
        public DataEntity Entity { get; set; }
        public List<UpdateRequest> UpdateFilter { get; set; }
        public EntityMetaData MetaData { get; set; }
        public List<WhereClause> WhereClauses { get; set; }
        #endregion
    }
}
