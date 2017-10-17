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
    public class Lift : BaseChildDomainModel<Lift, LiftDTO, LiftFolder, LiftFolderDTO>,
        IDetailDomainModel<LiftDTO, LiftDetailDTO>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public int ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        public LiftDetailDTO BuildDetailDTO()
        {
            return new LiftDetailDTO
            {
                Parent = Parent?.BuildDTO()
            };
        }

        public override LiftDTO BuildDTO(LiftFolderDTO parentDTO)
        {
            return new LiftDTO
            {
                ID = ID,
                Name = Name,
                ParentID = ParentID
            };
        }

        public override void PopulateFromDTO(LiftDTO dto, LiftFolder parent)
        {
            ID = dto.ID;
            Name = dto.Name;
            ParentID = dto.ParentID;
            Parent = parent;
        }
    }
}
