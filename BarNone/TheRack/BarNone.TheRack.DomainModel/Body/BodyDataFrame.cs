using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.TheRack.DomainModel.Body
{
    [Table("BodyDataFrame", Schema = "public")]
    public class BodyDataFrame : IDomainModel<BodyDataFrame>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        //[ForeignKey(]
        public int UserID { get; set; }

        public DateTime TimeOfFrame { get; set; }

        public List<Joint> Joints { get; set; }

        [ForeignKey("BodyDataID")]
        public int BodyDataID { get; set; }

        public BodyData BodyData { get; set; }

        //protected override BodyDataFrameDetailDTO OnBuildDetailDTO(ConvertConfig config)
        //{
        //    return new BodyDataFrameDetailDTO
        //    {
        //        Joints = Joints?.Select(j => j.CreateDTO(config)).ToList()
        //    };
        //}

        //protected override BodyDataFrameDTO OnBuildDTO()
        //{
        //    return new BodyDataFrameDTO
        //    {
        //        ID = ID,
        //        TimeOfFrame = TimeOfFrame
        //    };
        //}

        //protected override void OnPopulate(BodyDataFrameDTO dto, ConvertConfig config = null)
        //{
        //    ID = dto.ID;
        //    TimeOfFrame = dto.TimeOfFrame;
        //}

        //protected override void OnDetailPopulate(BodyDataFrameDetailDTO dto, ConvertConfig config = null)
        //{
        //    Joints = dto.Joints?.Select(j => Joint.CreateFromDTO(j, config)).ToList();
        //}
    }
}
