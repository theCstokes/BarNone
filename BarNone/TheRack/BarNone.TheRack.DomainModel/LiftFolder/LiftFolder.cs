using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("LiftFolder", Schema = "public")]
    public class LiftFolder : BaseDomainModel<LiftFolder>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public LiftFolder Parent { get; set; }

        public List<Lift> Lifts { get; set; }

        public List<LiftFolder> SubFolders { get; set; }
    }
}
