using TheRack.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess.SQLBuilders
{
    public class SQLWhere : ISQLBuilder
    {
        public string Execute(DataRequest request)
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
            //var whereExpression = request.Where.Body as BinaryExpression;
            //return $" {SQLConstants.WHERE} {ExpressionToSQL(whereExpression, request)}";
            return $" {SQLConstants.WHERE} {values}"; 
        }

        private string ExpressionToSQL(BinaryExpression obj, DataRequest request)
        {
            var left = obj.Left;
            var right = obj.Right;
            var SQLOperation = WhereOperation.GetSQLOperation(obj.NodeType.ToString());
            return $"{ParseExpression(left, request)} {SQLOperation} {ParseExpression(right, request)}";
        }

        #region Private Member(s).
        private string ParseExpression(Expression expression, DataRequest request)
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
