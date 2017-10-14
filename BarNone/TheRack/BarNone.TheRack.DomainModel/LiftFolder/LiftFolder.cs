using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("LiftFolder", Schema = "public")]
    public class LiftFolder : BaseChildDomainModel<LiftFolder, LiftFolderDTO, LiftFolder, LiftFolderDTO>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public LiftFolder Parent { get; set; }

        public List<Lift> Lifts { get; set; }

        public List<LiftFolder> SubFolders { get; set; }

        public override LiftFolderDTO BuildDTO(LiftFolderDTO parent)
        {
            return new LiftFolderDTO
            {
                ID = ID,
                Name = Name,
                Details = new LiftFolderDetailDTO
                {
                    Lifts = Lifts.Select(l => l.BuildDTO()).ToList(),
                    SubFolders = SubFolders.Select(s => s.BuildDTO()).ToList(),
                    Parent = parent
                }
            };
        }

        public override void PopulateFromDTO(LiftFolderDTO dto, LiftFolder parent)
        {
            ID = dto.ID;
            Name = dto.Name;
            Lifts = dto.Details?.Lifts == null ? 
                dto.Details.Lifts.Select(l => Lift.CreateFromDTO(l, this)).ToList() : null;
            SubFolders = dto.Details?.SubFolders == null ?
                dto.Details.SubFolders.Select(s => LiftFolder.CreateFromDTO(s)).ToList() : null;
            Parent = parent;
        }
    }
}
