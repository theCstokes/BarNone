using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    [Table("JointType", Schema = "public")]
    public class JointType : BaseEnumDomainModel<JointType, EJointType>
    {
        #region Public Constructor(s).
        public JointType(): base()
        {
        }

        public JointType(EJointType @enum): base(@enum)
        {
        }
        #endregion

        #region Public Operator Overload(s).
        public static implicit operator JointType(EJointType @enum) => new JointType(@enum);

        public static implicit operator EJointType(JointType joint) => (EJointType)joint.ID; 
        #endregion
    }
}
