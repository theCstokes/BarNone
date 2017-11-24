using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.TheRack.DomainModel
{
    [Table("LiftFolder", Schema = "public")]
    public class LiftFolder : IDomainModel<LiftFolder>
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        public List<Lift> Lifts { get; set; }

        public List<LiftFolder> SubFolders { get; set; }

        //protected override LiftFolderDetailDTO OnBuildDetailDTO(ConvertConfig config)
        //{
        //    return new LiftFolderDetailDTO
        //    {
        //        Lifts = Lifts?.Select(l => l.CreateDTO(config)).ToList(),
        //        SubFolders = SubFolders?.Select(f => f.CreateDTO(config)).ToList(),
        //        Parent = Parent?.CreateDTO(config)
        //    };
        //}

        //protected override LiftFolderDTO OnBuildDTO()
        //{
        //    return new LiftFolderDTO
        //    {
        //        ID = ID,
        //        Name = Name
        //    };
        //}

        //protected override void OnPopulate(LiftFolderDTO dto, ConvertConfig config = null)
        //{
        //    ID = dto.ID;
        //    Name = dto.Name;
        //}

        //protected override void OnDetailPopulate(LiftFolderDetailDTO dto, ConvertConfig config = null)
        //{
        //    Parent = LiftFolder.CreateFromDTO(config?.Parent, config);
        //    Lifts = dto.Lifts?.Select(l => Lift.CreateFromDTO(l, config)).ToList();
        //    SubFolders = dto.SubFolders?.Select(f => LiftFolder.CreateFromDTO(f, config)).ToList();
        //}

        //public override void SetParent(dynamic parent)
        //{
        //    Parent = parent;
        //}

        //public override dynamic GetParent()
        //{
        //    return Parent;
        //}
    }
}
