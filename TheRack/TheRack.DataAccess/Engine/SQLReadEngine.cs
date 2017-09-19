using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DomainModel;
using TheRack.Core.Extensions;

namespace TheRack.DataAccess.Engine
{
    public static partial class SQLEngine
    {
        private static string GenerateReadRequest<TDomainModel>(DataReadRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var values = new StringBuilder();
            request.Properties.ForEach((key, idx) =>
            {
                if (idx > 0)
                {
                    values.Append(SQLConstants.SEPARATOR);
                }
                values.Append($"\"{key}\"");
            });

            return $"{SQLConstants.SELECT} {values} {SQLConstants.FROM} \"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\"";
        }
    }
}
