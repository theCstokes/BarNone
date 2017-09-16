using TheRack.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess.SQLBuilders
{
    public class SQLGet : ISQLBuilder
    {
        public string Execute(DataRequest request)
        {
            var values = new StringBuilder();
            request.Entity.ForEach((pair, idx) =>
            {
                if (idx > 0)
                {
                    values.Append(SQLConstants.SEPARATOR);
                }
                values.Append($"\"{pair.Key}\"");
            });

            return $"{SQLConstants.SELECT} {values} {SQLConstants.FROM} \"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\"";
        }
    }
}
