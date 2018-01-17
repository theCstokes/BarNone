using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.TheRack.DomainModel.Body
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

        //protected override JointDetailDTO OnBuildDetailDTO(ConvertConfig config)
        //{
        //    return new JointDetailDTO
        //    {
        //        JointType = JointType?.CreateDTO(config),
        //        JointTrackingStateType = JointTrackingStateType?.CreateDTO(config),
        //        BodyDataFrame = BodyDataFrame?.CreateDTO(config)
        //    };
        //}

        //protected override JointDTO OnBuildDTO()
        //{
        //    return new JointDTO
        //    {
        //        ID = ID,
        //        X = X,
        //        Y = Y,
        //        Z = Z,
        //        JointTypeID = JointTypeID,
        //        JointTrackingStateTypeID = JointTrackingStateTypeID,
        //        BodyDataFrameID = BodyDataFrameID
        //    };
        //}

        //protected override void OnPopulate(JointDTO dto, ConvertConfig config = null)
        //{
        //    ID = dto.ID;
        //    X = dto.X;
        //    Y = dto.Y;
        //    Z = dto.Z;
        //    JointTypeID = dto.JointTypeID;
        //    JointTrackingStateTypeID = dto.JointTrackingStateTypeID;
        //}

        //protected override void OnDetailPopulate(JointDetailDTO dto, ConvertConfig config = null)
        //{
        //    JointType = JointType.CreateFromDTO(dto.JointType, config);
        //    JointTrackingStateType = JointTrackingState.CreateFromDTO(dto.JointTrackingStateType, config);
        //}
    }
}
