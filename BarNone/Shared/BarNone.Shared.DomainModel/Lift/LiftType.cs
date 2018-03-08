using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("LiftType", Schema = "public")]
    public class LiftType : IDomainModel<LiftType>
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
