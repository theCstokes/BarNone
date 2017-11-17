using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.TheRack.DomainModel
{
    [Table("Lift", Schema = "public")]
    public class Lift : BaseDetailDomainModel<Lift, LiftDTO, LiftDetailDTO>
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
    }
}
