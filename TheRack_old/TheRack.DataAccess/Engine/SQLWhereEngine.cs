using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DomainModel;
using TheRack.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace TheRack.DataAccess.Engine
{
    public static partial class SQLEngine
    {
        private static string GenerateWhereRequest<TDomainModel>(IDataRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var values = new StringBuilder();
            request.WhereClauses.ForEach((where, idx) =>
            {
                if (idx > 0)
                {
                    values.Append(SQLConstants.SEPARATOR);
                }
                values.Append($"\"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"{where.Name}\"{where.Operation.SQLOperation}'{where.Value}' ");
                if (where.Operation != null)
                {
                    values.Append($"{where.Then} ");
                }
            });
            
            return $" {SQLConstants.WHERE} {values}";
        }

        #region Private Member(s).
        private static string ExpressionToSQL(BinaryExpression obj, DataRequest request)
        {
            var left = obj.Left;
            var right = obj.Right;
            var SQLOperation = WhereOperation.GetSQLOperation(obj.NodeType.ToString());
            return $"{ParseExpression(left, request)} {SQLOperation} {ParseExpression(right, request)}";
        }
        private static string ParseExpression(Expression expression, DataRequest request)
        {
            if (expression is BinaryExpression)
            {
                return ExpressionToSQL(expression as BinaryExpression, request);
            }

            try
            {
                var value = Expression.Lambda(expression).Compile().DynamicInvoke().ToString();
                return $"'{value}'";
            }
            catch (InvalidOperationException)
            {
                PropertyInfo property = ((MemberExpression)expression).Member as PropertyInfo;
                return $"\"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"{property.Name}\"";
            }
        }
        #endregion
    }
}
