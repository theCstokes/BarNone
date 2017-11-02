using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
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

        #region Public Property(s).
        [Key]
        public override int ID { get; set; }

        public override int EnumID { get; set; }

        public override string Name { get; set; }
        #endregion

        #region Public Operator Overload(s).
        public static implicit operator JointType(EJointType @enum) => new JointType(@enum);

        public static implicit operator EJointType(JointType joint) => (EJointType)joint.ID; 
        #endregion
    }
}
