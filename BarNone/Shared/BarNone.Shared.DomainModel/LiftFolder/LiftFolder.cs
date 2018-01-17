using BarNone.Shared.DomainModel.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.Shared.DomainModel
{
    [Table("LiftFolder", Schema = "public")]
    public class LiftFolder : IDomainModel<LiftFolder>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        public List<Lift> Lifts { get; set; }

        public List<LiftFolder> SubFolders { get; set; }
    }
}
