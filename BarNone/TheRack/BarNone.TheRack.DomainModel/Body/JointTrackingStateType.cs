using BarNone.Shared.DataTransfer.LiftData;
using BarNone.TheRack.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.TheRack.DomainModel.Body
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
