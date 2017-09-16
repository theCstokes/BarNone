using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class WhereClauseBuilder<T>
    {
        public static List<WhereClause> Execute(Expression<Func<T, bool>> where, DataRequest request)
        {
            //Expression<Func<object, bool>> expression = x => where(x);
            return Execute(where.Body as BinaryExpression, request);
        }
        private static List<WhereClause> Execute(BinaryExpression obj, DataRequest request)
        {
            var whereClauses = new List<WhereClause>();
            var left = obj.Left;
            var right = obj.Right;

            if (left is BinaryExpression && right is BinaryExpression)
            {
                whereClauses = whereClauses
                    .Concat(Execute(left as BinaryExpression, request).AsEnumerable())
                    .Concat(Execute(right as BinaryExpression, request).AsEnumerable())
                    .ToList();

            } else
            {
                var clause = new WhereClause();

                try
                {
                    clause.Name = Expression.Lambda(left).Compile().DynamicInvoke().ToString();
                }
                catch (InvalidOperationException)
                {
                    PropertyInfo property = ((MemberExpression)left).Member as PropertyInfo;
                    clause.Name = property.Name;
                }

                try
                {
                    clause.Value = Expression.Lambda(right).Compile().DynamicInvoke().ToString();
                }
                catch (InvalidOperationException)
                {
                    PropertyInfo property = ((MemberExpression)right).Member as PropertyInfo;
                    clause.Value = property.Name;
                }

                clause.Operation = WhereOperation.GetOperation(obj.NodeType.ToString());
                whereClauses.Add(clause);
            }

            return whereClauses;
        }

        #region Private Member(s).
        //private string ParseExpression(Expression expression, DataRequest request)
        //{
        //    if (expression is BinaryExpression)
        //    {
        //        return ExpressionToSQL(expression as BinaryExpression, request);
        //    }

        //    try
        //    {
        //        var value = Expression.Lambda(expression).Compile().DynamicInvoke().ToString();
        //        return $"'{value}'";
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        PropertyInfo property = ((MemberExpression)expression).Member as PropertyInfo;
        //        return $"\"{request.MetaData.SchemaName}\".\"{request.MetaData.TableName}\".\"{property.Name}\"";
        //    }
        //}
        #endregion
    }
}
