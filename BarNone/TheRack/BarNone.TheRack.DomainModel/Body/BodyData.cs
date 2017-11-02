using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DomainModel.Body;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("BodyData", Schema = "public")]
    public class BodyData : BaseDomainModel<BodyData, BodyDataDTO>
    {
        [Key]
        public override int ID { get; set; }

        public DateTime RecordDate { get; set; }

        public List<BodyDataFrame> BodyDataFrames { get; set; }

        public override BodyDataDTO BuildDTO()
        {
            throw new NotImplementedException();
        }

        public override void PopulateFromDTO(BodyDataDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
