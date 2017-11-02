using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
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

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float PositionZ { get; set; }

        public int BodyDataFrameID { get; set; }

        [ForeignKey("BodyDataFrameID")]
        public BodyDataFrame BodyDataFrame { get; set; }

        public JointTrackingState TrackingState { get; set; }

        public override JointDTO BuildDTO()
        {
            throw new NotImplementedException();
        }

        public override void PopulateFromDTO(JointDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
