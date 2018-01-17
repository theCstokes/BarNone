using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.Shared.DomainModel
{
    [Table("BodyData", Schema = "public")]
    public class BodyData : IDomainModel<BodyData>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        public DateTime RecordDate { get; set; }

        public List<BodyDataFrame> BodyDataFrames { get; set; }
    }
}
