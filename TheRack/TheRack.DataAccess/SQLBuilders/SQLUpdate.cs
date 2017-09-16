using TheRack.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess.SQLBuilders
{
    public class SQLUpdate : ISQLBuilder
    {
        public string Execute(DataRequest request)
        {
            if(request.Type != RequestType.UPDATE) return null; // TODO - Errors.
            if (request.Entity.ID == 0) return null; // TODO - Errors.
            if (request.UpdateFilter.Count <= 0) return null; // TODO - Errors.

            var builder = new StringBuilder($"{SQLConstants.UPDATE} \"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\"");
            var values = new StringBuilder();
            request.Entity
                .ForEach((pair, idx) =>
                {
                    var filter = request.UpdateFilter.Find(f =>
                        {
                            return (f.Name.ToLower() == pair.Key.ToLower());
                        });

                    if (filter == null) return;

                    if (idx > 0)
                    {
                        values.Append(SQLConstants.SEPARATOR);
                    }

                    if (filter.Operation ==  null)
                    {
                        values.Append($"\"{pair.Key}\"='{pair.Value}'");
                    } else
                    {
                        values.Append($"\"{pair.Key}\"=\"{pair.Key}\"{filter.Operation.SQLOperation}'{pair.Value}'");
                    }
                });

            builder.Append($"{SQLConstants.SET} {values} {SQLConstants.WHERE} \"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"ID\"={request.Entity.ID}");
            return builder.ToString();
        }
    }
}
