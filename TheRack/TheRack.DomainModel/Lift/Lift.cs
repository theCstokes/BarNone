using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheRack.DomainModel
{
    [Table("Lift", Schema = "public")]
    public class Lift : BaseDomainModel<Lift>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public int ParentID { get; set; }

        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }
    }
}
