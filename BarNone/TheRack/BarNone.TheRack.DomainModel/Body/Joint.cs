using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel.Body
{
    [Table("Joint", Schema = "public")]
    public class Joint : BaseDomainModel<Joint, JointDTO>
    {
        public override int ID { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public int BodyDataFrameID { get; set; }

        [ForeignKey("BodyDataFrameID")]
        public BodyDataFrame BodyDataFrame { get; set; }

        public JointTrackingState TrackingState { get; set; }

        //public override JointDTO BuildDTO()
        //{
        //    return new JointDTO
        //    {
        //        ID = ID,
        //        PositionX = PositionX,
        //        PositionY = PositionY,
        //        PositionZ = PositionZ,
        //        TrackingState = TrackingState.BuildDTO()
        //    };
        //}

        //public override void PopulateFromDTO(JointDTO dto)
        //{
        //    throw new NotImplementedException();
        //}

        protected override JointDTO OnBuildDTO()
        {
            return new JointDTO
            {
                ID = ID,
                PositionX = X,
                PositionY = Y,
                PositionZ = Z,
                TrackingState = TrackingState?.CreateDTO()
            };
        }

        protected override void OnPopulate(JointDTO dto, ConvertConfig config = null)
        {
            ID = dto.ID;
            X = dto.PositionX;
            Y = dto.PositionY;
            Z = dto.PositionZ;
            TrackingState = JointTrackingState.CreateFromDTO(dto.TrackingState);
        }
    }
}
