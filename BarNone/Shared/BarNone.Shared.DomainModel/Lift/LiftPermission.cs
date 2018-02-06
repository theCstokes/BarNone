using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("LiftPermission", Schema = "public")]
    public class LiftPermission : IDomainModel<LiftPermission>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int LiftID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("LiftID")]
        public Lift Lift { get; set; }
    }
}
