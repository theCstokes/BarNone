using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    [Table("JointTrackingStateType", Schema = "public")]
    public class JointTrackingStateType 
        : BaseEnumDomainModel<JointTrackingStateType, EJointTrackingStateType>
    {
        #region Public Constructor(s).
        public JointTrackingStateType(): base()
        {
        }

        public JointTrackingStateType(EJointTrackingStateType @enum): base(@enum)
        {
        }
        #endregion

        #region Public Operator Overload(s).
        public static implicit operator JointTrackingStateType(EJointTrackingStateType @enum) => new JointTrackingStateType(@enum);

        public static implicit operator EJointTrackingStateType(JointTrackingStateType joint) => (EJointTrackingStateType)joint.ID;
        #endregion
    }
}
