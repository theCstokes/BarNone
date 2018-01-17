using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.TheRack.DomainModel
{
    [Table("Lift", Schema = "public")]
    public class Lift : IDomainModel<Lift>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        //[Key]
        public int UserID { get; set; }

        public string Name { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        public int? BodyDataID { get; set; }

        public BodyData BodyData { get; set; }

        public int VideoID { get; set; }

        [ForeignKey("VideoID")]
        public VideoRecord Video { get; set; }
    }
}
