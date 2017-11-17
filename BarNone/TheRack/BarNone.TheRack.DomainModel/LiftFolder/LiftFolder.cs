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
    public class LiftFolder : DetailDomainModel<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO>
        //IDetailDomainModel<LiftFolderDTO, LiftFolderDetailDTO>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        public List<Lift> Lifts { get; set; }

        public List<LiftFolder> SubFolders { get; set; }

        protected override LiftFolderDetailDTO OnBuildDetailDTO(ConvertConfig config)
        {
            return new LiftFolderDetailDTO
            {
                Lifts = Lifts?.Select(l => l.CreateDTO(config)).ToList(),
                SubFolders = SubFolders?.Select(f => f.CreateDTO(config)).ToList(),
                Parent = Parent?.CreateDTO(config)
            };
        }

        protected override LiftFolderDTO OnBuildDTO()
        {
            return new LiftFolderDTO
            {
                ID = ID,
                Name = Name
            };
        }

        protected override void OnPopulate(LiftFolderDTO dto, ConvertConfig config = null)
        {
            ID = dto.ID;
            Name = dto.Name;

            Lifts = dto.Details?.Lifts.Select(l => Lift.CreateFromDTO(l)).ToList();
            SubFolders = dto.Details?.SubFolders.Select(f => LiftFolder.CreateFromDTO(f, config)).ToList();

            // Use parent chain.
            Parent = config.Parent;
        }

        //public LiftFolderDetailDTO BuildDetailDTO()
        //{
        //    return new LiftFolderDetailDTO
        //    {
        //        Lifts = Lifts?.Select(l => l.BuildDTO()).ToList(),
        //        SubFolders = SubFolders?.Select(s => s.BuildDTO()).ToList(),
        //        Parent = Parent?.BuildDTO()
        //    };
        //}

        //public override LiftFolderDTO BuildDTO(LiftFolderDTO parent)
        //{
        //    return new LiftFolderDTO
        //    {
        //        ID = ID,
        //        Name = Name
        //    };
        //}

        //public override void PopulateFromDTO(LiftFolderDTO dto, LiftFolder parent)
        //{
        //    ID = dto.ID;
        //    Name = dto.Name;
        //    Lifts = dto.Details?.Lifts != null ? 
        //        dto.Details.Lifts.Select(l => Lift.CreateFromDTO(l, this)).ToList() : null;
        //    SubFolders = dto.Details?.SubFolders != null ?
        //        dto.Details.SubFolders.Select(s => LiftFolder.CreateFromDTO(s)).ToList() : null;
        //    Parent = parent;
        //}

        //dynamic IDetailDomainModel.BuildDetailDTO()
        //{
        //    return BuildDetailDTO();
        //}
    }
}
