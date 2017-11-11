using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DomainModel.Body;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("BodyData", Schema = "public")]
    public class BodyData : BaseDomainModel<BodyData, BodyDataDTO>, IParentDomainModel<BodyDataDTO, BodyDataDetailDTO>
    {
        [Key]
        public override int ID { get; set; }

        public DateTime RecordDate { get; set; }

        public List<BodyDataFrame> BodyDataFrames { get; set; }

        public dynamic BuildDetailDTO()
        {
            return new BodyDataDetailDTO
            {
                OrderedFrames = BodyDataFrames.Select(f => f.BuildDTO()).ToList()
            };
        }

        public override BodyDataDTO BuildDTO()
        {
            return new BodyDataDTO
            {
                ID = ID,
                RecordTimeStamp = RecordDate
            };
        }

        public override void PopulateFromDTO(BodyDataDTO dto)
        {
            ID = dto.ID;
            RecordDate = dto.RecordTimeStamp;
        }

        BodyDataDetailDTO IParentDomainModel<BodyDataDTO, BodyDataDetailDTO>.BuildDetailDTO()
        {
            return BuildDetailDTO();
        }
    }
}
