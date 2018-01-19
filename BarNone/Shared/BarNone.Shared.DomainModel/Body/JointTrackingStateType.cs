using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Joint tracking state domain model.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.BaseEnumDomainModel{BarNone.Shared.DomainModel.JointTrackingStateType, BarNone.Shared.DomainModel.EJointTrackingStateType}" />
    [Table("JointTrackingStateType", Schema = "public")]
    public class JointTrackingStateType 
        : BaseEnumDomainModel<JointTrackingStateType, EJointTrackingStateType>
    {
        #region Public Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="JointTrackingStateType"/> class.
        /// </summary>
        public JointTrackingStateType(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JointTrackingStateType"/> class.
        /// </summary>
        /// <param name="enum">The enum.</param>
        public JointTrackingStateType(EJointTrackingStateType @enum): base(@enum)
        {
        }
        #endregion

        #region Public Operator Overload(s).        
        /// <summary>
        /// Performs an implicit conversion from <see cref="EJointTrackingStateType"/> to <see cref="JointTrackingStateType"/>.
        /// </summary>
        /// <param name="enum">The enum.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator JointTrackingStateType(EJointTrackingStateType @enum) => new JointTrackingStateType(@enum);

        /// <summary>
        /// Performs an implicit conversion from <see cref="JointTrackingStateType"/> to <see cref="EJointTrackingStateType"/>.
        /// </summary>
        /// <param name="joint">The joint.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator EJointTrackingStateType(JointTrackingStateType joint) => (EJointTrackingStateType)joint.ID;
        #endregion
    }
}
