using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DomainModel.Core
{
    public abstract class BaseEnumDomainModel<TType, TEType> : IDomainModel
        where TType : BaseEnumDomainModel<TType, TEType>, new()
        where TEType : struct
    {
        #region Public Constructor(s).
        public BaseEnumDomainModel()
        {

        }

        public BaseEnumDomainModel(TEType @enum)
        {
            var value = Convert.ToInt32(@enum);
            ID = value + 1;
            Value = value;
            Name = @enum.ToString();
        }
        #endregion

        #region Public Property(s).
        public abstract int ID { get; set; }

        public abstract int Value { get; set; }

        public abstract string Name { get; set; }
        #endregion

        #region Public Operator Overload(s).
        //public static implicit operator TType(TEType @enum) => new TType(@enum);

        //public static implicit operator TEType(TType joint) => joint.ID;
        #endregion

        #region IDetailDomainModel Implementation.
        dynamic IDomainModel.BuildDTO()
        {
            return Value;
        }
        #endregion
    }
}
