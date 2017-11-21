using BarNone.Shared.DataTransfer.LiftData;
using BarNone.TheRack.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.TheRack.DomainModel.Body
{
    [Table("JointTrackingState", Schema = "public")]
    public class JointTrackingState : BaseEnumDomainModel<JointTrackingState, EJointTrackingState, JointTrackingStateTypeDTO>
    {
        #region Public Constructor(s).
        public JointTrackingState(): base()
        {
        }

        public JointTrackingState(EJointTrackingState @enum): base(@enum)
        {
        }
        #endregion

        #region Public Property(s).
        [Key]
        public override int ID { get; set; }

        public override int Value { get; set; }

        public override string Name { get; set; }
        #endregion

        #region Public Operator Overload(s).
        public static implicit operator JointTrackingState(EJointTrackingState @enum) => new JointTrackingState(@enum);

        public static implicit operator EJointTrackingState(JointTrackingState joint) => (EJointTrackingState)joint.ID;
        #endregion
    }
}
