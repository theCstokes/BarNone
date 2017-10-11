using BarNone.TheRack.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DataAdapter.Core
{
    public class BasePropertyEnum : MapEnum<BasePropertyEnum>
    {
        public delegate IPropertyMapper PropertyMapperAction();

        public static readonly BasePropertyEnum ID = new BasePropertyEnum("ID", () => new BasicPropertyMapper());

        //#region Public Static Instance(s).
        //public static readonly BasePropertyEnum AND = new BasePropertyEnum(0, "AND", "And", "AndAlso");
        //public static readonly BasePropertyEnum OR = new BasePropertyEnum(1, "OR", "Or", "OrElse");
        //public static readonly BasePropertyEnum GREATER_THEN = new BasePropertyEnum(2, ">", "GreaterThan");
        //public static readonly BasePropertyEnum EQUAL = new BasePropertyEnum(3, "=", "Equal");
        //public static readonly BasePropertyEnum ADD = new BasePropertyEnum(4, "+", "Add");
        //public static readonly BasePropertyEnum SUBTRACT = new BasePropertyEnum(5, "-", "Subtract");
        //#endregion

        private PropertyMapperAction _action;

        #region Private Constructor(s).
        public BasePropertyEnum(string name, PropertyMapperAction action) : base()
        {
            Name = name;
            _action = action;
            //Mapper = mapper;
        }
        #endregion

        #region Public Property(s).
        public string Name { get; set; }

        public IPropertyMapper Mapper
        {
            get
            {
                return _action();
            }
        }
        #endregion

        //#region Public Static Member(s).
        //public static string GetSQLOperation(string expressionOpperation)
        //{
        //    return Types.Values.First(type => type.ExpressionOperations.Contains(expressionOpperation)).SQLOperation;
        //}

        //public static BasePropertyEnum GetOperation(string expressionOpperation)
        //{
        //    return Types.Values.First(type => type.ExpressionOperations.Contains(expressionOpperation));
        //}
        //#endregion

        #region Mapper Implementation.
        protected override BasePropertyEnum Instance
        {
            get
            {
                return this;
            }
        }
        #endregion
    }
}
