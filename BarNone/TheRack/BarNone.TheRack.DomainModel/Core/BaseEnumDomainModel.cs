using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.TheRack.DomainModel.Core;


namespace BarNone.TheRack.DomainModel.Core
{
    public abstract class BaseEnumDomainModel<TType, TEType, TDTO> : BaseDomainModel<TType, TDTO>
        where TType : BaseEnumDomainModel<TType, TEType, TDTO>, new()
        where TEType : struct
        where TDTO : BaseTypeDTO<TDTO>, new()
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
        //public abstract int ID { get; set; }

        public abstract int Value { get; set; }

        public abstract string Name { get; set; }
        public override int ID { get; set; }
        #endregion

        #region Public Operator Overload(s).
        //public static implicit operator TType(TEType @enum) => new TType(@enum);

        //public static implicit operator TEType(TType joint) => joint.ID;
        #endregion

        #region IDetailDomainModel Implementation.
        protected override TDTO OnBuildDTO()
        {
            return new TDTO
            {
                ID = ID,
                Value = Value,
                Name = Name
            };
        }

        protected override void OnPopulate(TDTO dto, ConvertConfig config = null)
        {
            ID = dto.ID;
            Value = dto.Value;
            Name = dto.Name;
        }
        #endregion
    }
}
