using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel.Body
{
    [Table("Body", Schema = "public")]
    public class BodyDataFrame : BaseDomainModel<BodyDataFrame, BodyDataFrameDTO>
    {
        [Key]
        public override int ID { get; set; }

        public DateTime TimeOfFrame { get; set; }

        public List<Joint> Joints { get; set; }

        [ForeignKey("BodyDataID")]
        public int BodyDataID { get; set; }

        public BodyData BodyData { get; set; }

        public override BodyDataFrameDTO BuildDTO()
        {
            throw new NotImplementedException();
        }

        public override void PopulateFromDTO(BodyDataFrameDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
