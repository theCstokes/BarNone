using BarNone.Shared.DomainModel.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace BarNone.Shared.DomainModel.Core
{
    /// <summary>
    /// Base enum domain model.
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <typeparam name="TEType">The type of the e type.</typeparam>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{TType}" />
    public abstract class BaseEnumDomainModel<TType, TEType> : IDomainModel<TType>
        where TType : BaseEnumDomainModel<TType, TEType>, new()
        where TEType : struct
    {
        #region Public Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEnumDomainModel{TType, TEType}"/> class.
        /// </summary>
        public BaseEnumDomainModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEnumDomainModel{TType, TEType}"/> class.
        /// </summary>
        /// <param name="enum">The enum.</param>
        public BaseEnumDomainModel(TEType @enum)
        {
            var value = Convert.ToInt32(@enum);
            ID = value + 1;
            Value = value;
            Name = @enum.ToString();
        }
        #endregion

        #region Public Property(s).        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        #endregion
    }
}
