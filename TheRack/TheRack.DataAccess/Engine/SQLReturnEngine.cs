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
        private static string GenerateReturnRequest<TDomainModel>(IDataRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var values = new StringBuilder();
            request.Properties.ForEach(filter =>
            {
                //if (filter.Operation == null) return;

                values.Append($"\"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"{filter}\"");
            });
            return $"{SQLConstants.RETURNING} {values}";
        }
    }
}
