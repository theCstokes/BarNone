using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Body;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.TheRack.DomainModel
{
    [Table("BodyData", Schema = "public")]
    public class BodyData : IDomainModel<BodyData>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        //[Key]
        public int UserID { get; set; }

        public DateTime RecordDate { get; set; }

        public List<BodyDataFrame> BodyDataFrames { get; set; }

        //protected override BodyDataDTO OnBuildDTO()
        //{
        //    return new BodyDataDTO
        //    {
        //        ID = ID,
        //        RecordTimeStamp = RecordDate
        //    };
        //}

        //protected override BodyDataDetailDTO OnBuildDetailDTO(ConvertConfig config)
        //{
        //    return new BodyDataDetailDTO
        //    {
        //        OrderedFrames = BodyDataFrames.Select(frame => frame.CreateDTO(config)).ToList()
        //    };
        //}

        //protected override void OnPopulate(BodyDataDTO dto, ConvertConfig config = null)
        //{
        //    ID = dto.ID;
        //    RecordDate = dto.RecordTimeStamp;
        //}

        //protected override void OnDetailPopulate(BodyDataDetailDTO dto, ConvertConfig config = null)
        //{
        //    BodyDataFrames = dto.OrderedFrames?.Select(frame => BodyDataFrame.CreateFromDTO(frame, config)).ToList();
        //}
    }
}
