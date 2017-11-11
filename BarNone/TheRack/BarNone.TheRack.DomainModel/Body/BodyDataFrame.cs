using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DomainModel.Body
{
    [Table("BodyDataFrame", Schema = "public")]
    public class BodyDataFrame : BaseDomainModel<BodyDataFrame, BodyDataFrameDTO>, 
        IParentDomainModel<BodyDataFrameDTO, BodyDataFrameDetailDTO>
    {
        [Key]
        public override int ID { get; set; }

        public DateTime TimeOfFrame { get; set; }

        public List<Joint> Joints { get; set; }

        [ForeignKey("BodyDataID")]
        public int BodyDataID { get; set; }

        public BodyData BodyData { get; set; }

        public BodyDataFrameDetailDTO BuildDetailDTO()
        {
            return new BodyDataFrameDetailDTO
            {
                Joints = Joints.Select(j => j.BuildDTO()).ToList()
            };
        }

        public override BodyDataFrameDTO BuildDTO()
        {
            return new BodyDataFrameDTO
            {
                ID = ID,
                TimeOfFrame = TimeOfFrame
            };
        }

        public override void PopulateFromDTO(BodyDataFrameDTO dto)
        {
            throw new NotImplementedException();
        }

        dynamic IParentDomainModel.BuildDetailDTO()
        {
            return BuildDetailDTO();
        }
    }
}
