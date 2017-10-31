using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("JointType", Schema = "public")]
    public class Joint : IDomainModel
    {
        #region Public Constructor(s).
        public Joint(JointType @enum)
        {
            ID = (int)@enum;
            EnumID = ID + 1;
            Name = @enum.ToString();
        } 
        #endregion

        #region Public Property(s).
        [Key]
        public int ID { get; set; }

        public int EnumID { get; set; }

        public string Name { get; set; }
        #endregion

        #region Public Operator Overload(s).
        public static implicit operator Joint(JointType @enum) => new Joint(@enum);

        public static implicit operator JointType(Joint joint) => (JointType)joint.ID; 
        #endregion
    }
}
