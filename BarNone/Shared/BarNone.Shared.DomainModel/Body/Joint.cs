using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    [Table("Joint", Schema = "public")]
    public class Joint : IDomainModel<Joint>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        //[Key]
        public int UserID { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public int BodyDataFrameID { get; set; }

        [ForeignKey("BodyDataFrameID")]
        public BodyDataFrame BodyDataFrame { get; set; }

        public int JointTypeID { get; set; }

        [ForeignKey("JointTypeID")]
        public JointType JointType { get; set; }

        public int JointTrackingStateTypeID { get; set; }

        [ForeignKey("JointTrackingStateTypeID")]
        public JointTrackingStateType JointTrackingStateType { get; set; }
    }
}
