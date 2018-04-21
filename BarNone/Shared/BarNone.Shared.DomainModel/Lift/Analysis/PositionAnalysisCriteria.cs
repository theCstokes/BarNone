using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("PositionAnalysisCriteria", Schema = "public")]
    public class PositionAnalysisCriteria : IDomainModel<PositionAnalysisCriteria>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int JointTypeID { get; set; }

        [ForeignKey("JointTypeID")]
        public JointType JointType { get; set; }
    }
}
