using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.Core.Extensions;
using TheRack.DataAccess;
using TheRack.DomainModel;

namespace TheRack.DataAccess.Engine
{
    public static partial class SQLEngine
    {
        public static string GenerateCreateRequest<TDomainModel>(DataCreateRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            if (request.Type != RequestType.CREATE) return null; // TODO - Errors.
            var builder = new StringBuilder($"{SQLConstants.INSERT} \"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\"");

            var update = new StringBuilder();
            var values = new StringBuilder();
            request.Entity.ForEach((pair, idx) =>
            {
                if (idx > 0)
                {
                    update.Append(SQLConstants.SEPARATOR);
                    values.Append(SQLConstants.SEPARATOR);
                }
                update.Append($"\"{pair.Key}\"");
                values.Append($"'{pair.Value}'");
            });

            builder.Append($"({update}) {SQLConstants.VALUES}({values}) {SQLConstants.RETURNING} \"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"ID\"");
            return builder.ToString();
        }
    }
}
