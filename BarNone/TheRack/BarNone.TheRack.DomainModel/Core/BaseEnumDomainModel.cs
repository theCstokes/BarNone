using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace BarNone.TheRack.DomainModel.Core
{
    public abstract class BaseEnumDomainModel<TType, TEType> : IDomainModel<TType>
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
        [Key]
        public int ID { get; set; }

        public int Value { get; set; }

        public string Name { get; set; }
        #endregion
    }
}
