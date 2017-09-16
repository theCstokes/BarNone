using TheRack.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class WhereOperation : Mapper<WhereOperation>
    {
        #region Public Static Instance(s).
        public static readonly WhereOperation AND = new WhereOperation(0, "AND", "And", "AndAlso");
        public static readonly WhereOperation OR = new WhereOperation(1, "OR", "Or", "OrElse");
        public static readonly WhereOperation GREATER_THEN = new WhereOperation(2, ">", "GreaterThan");
        public static readonly WhereOperation EQUAL = new WhereOperation(3, "=", "Equal");
        public static readonly WhereOperation ADD = new WhereOperation(4, "+", "Add");
        public static readonly WhereOperation SUBTRACT = new WhereOperation(5, "-", "Subtract");
        #endregion

        #region Private Constructor(s).
        private WhereOperation(int value, string sqlOperation, params string[] expressionOperation) : base(value)
        {
            SQLOperation = sqlOperation;
            ExpressionOperations = expressionOperation;
        }
        #endregion

        #region Public Property(s).
        public string SQLOperation { get; private set; }
        public string[] ExpressionOperations { get; private set; }
        #endregion

        #region Public Static Member(s).
        public static string GetSQLOperation(string expressionOpperation)
        {
            return Types.Values.First(type => type.ExpressionOperations.Contains(expressionOpperation)).SQLOperation;
        }

        public static WhereOperation GetOperation(string expressionOpperation)
        {
            return Types.Values.First(type => type.ExpressionOperations.Contains(expressionOpperation));
        }
        #endregion

        #region Mapper Implementation.
        protected override WhereOperation Instance
        {
            get
            {
                return this;
            }
        }
        #endregion
    }
}
