using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Joint type domain model
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.BaseEnumDomainModel{BarNone.Shared.DomainModel.JointType, BarNone.Shared.DomainModel.EJointType}" />
    [Table("JointType", Schema = "public")]
    public class JointType : BaseEnumDomainModel<JointType, EJointType>
    {
        #region Public Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="JointType"/> class.
        /// </summary>
        public JointType(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JointType"/> class.
        /// </summary>
        /// <param name="enum">The enum.</param>
        public JointType(EJointType @enum): base(@enum)
        {
        }
        #endregion

        #region Public Operator Overload(s).        
        /// <summary>
        /// Performs an implicit conversion from <see cref="EJointType"/> to <see cref="JointType"/>.
        /// </summary>
        /// <param name="enum">The enum.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator JointType(EJointType @enum) => new JointType(@enum);

        /// <summary>
        /// Performs an implicit conversion from <see cref="JointType"/> to <see cref="EJointType"/>.
        /// </summary>
        /// <param name="joint">The joint.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator EJointType(JointType joint) => (EJointType)joint.Value; 
        #endregion
    }
}
