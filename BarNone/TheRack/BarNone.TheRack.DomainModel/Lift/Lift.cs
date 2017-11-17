using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("Lift", Schema = "public")]
    public class Lift : DetailDomainModel<Lift, LiftDTO, LiftDetailDTO>
        //IDetailDomainModel<LiftDTO, LiftDetailDTO>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public int ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        protected override LiftDetailDTO OnBuildDetailDTO(ConvertConfig config)
        {
            return new LiftDetailDTO
            {
                Parent = Parent.CreateDTO(config)
            };
        }

        protected override LiftDTO OnBuildDTO()
        {
            return new LiftDTO
            {
                ID = ID,
                Name = Name
            };
        }

        protected override void OnPopulate(LiftDTO dto, ConvertConfig config = null)
        {
            ID = dto.ID;
            Name = dto.Name;

            // Use Parent Chain.
            Parent = config?.Parent;
        }

        //public LiftDetailDTO BuildDetailDTO()
        //{
        //    return new LiftDetailDTO
        //    {
        //        Parent = Parent?.BuildDTO()
        //    };
        //}

        //public override LiftDTO BuildDTO(LiftFolderDTO parentDTO)
        //{
        //    return new LiftDTO
        //    {
        //        ID = ID,
        //        Name = Name,
        //        ParentID = ParentID
        //    };
        //}

        //public override void PopulateFromDTO(LiftDTO dto, LiftFolder parent)
        //{
        //    ID = dto.ID;
        //    Name = dto.Name;
        //    ParentID = dto.ParentID;
        //    Parent = parent;
        //}

        //#region IDetailDomainModel Implementation.
        //dynamic IDetailDomainModel.BuildDetailDTO()
        //{
        //    return BuildDetailDTO();
        //}
        //#endregion
    }
}
