using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.Shared.DomainModel
{
    [Table("BodyDataFrame", Schema = "public")]
    public class BodyDataFrame : IDomainModel<BodyDataFrame>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        public DateTime TimeOfFrame { get; set; }

        public List<Joint> Joints { get; set; }

        [ForeignKey("BodyDataID")]
        public int BodyDataID { get; set; }

        public BodyData BodyData { get; set; }
    }
}
