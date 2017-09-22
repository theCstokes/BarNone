using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess.SQLBuilders
{
    public class SQLReturn : ISQLBuilder
    {
        public string Execute(DataRequest request)
        {
            var values = new StringBuilder();
            request.UpdateFilter.ForEach(filter =>
            {
                if (filter.Operation == null) return;

                values.Append($"\"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"{filter.Name}\"");
            });
            return $"{SQLConstants.RETURNING} {values}";
        }
    }
}
