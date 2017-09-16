using TheRack.Core.Extensions;
using TheRack.DataAccess.SQLBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public static class SQLGenerator
    {
        public static string Execute(DataRequest request)
        {
            var sb = new StringBuilder();
            switch (request.Type)
            {
                case RequestType.CREATE:
                    var create = new SQLCreate();
                    sb.Append(create.Execute(request));
                    break;
                case RequestType.UPDATE:
                    var update = new SQLUpdate();
                    sb.Append(update.Execute(request));
                    break;
                case RequestType.LOAD:
                    var get = new SQLGet();
                    sb.Append(get.Execute(request));
                    break;
                default:
                    break;
            }
            // TODO - Better where.
            if (request.WhereClauses != null && request.Type == RequestType.LOAD)
            {
                var where = new SQLWhere();
                sb.Append($" {where.Execute(request)}");
            }
            if (request.Type == RequestType.UPDATE)
            {
                var returning = new SQLReturn();
                sb.Append($" {returning.Execute(request)}");
            }
            return sb.ToString();
        }
    }
}
