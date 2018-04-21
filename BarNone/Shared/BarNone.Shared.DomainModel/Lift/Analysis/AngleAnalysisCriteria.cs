using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("AngleAnalysisCriteria", Schema = "public")]
    public class AngleAnalysisCriteria : IDomainModel<AngleAnalysisCriteria>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int JointTypeAID { get; set; }

        public int JointTypeBID { get; set; }

        public int JointTypeCID { get; set; }

        [ForeignKey("JointTypeAID")]
        public JointType JointTypeA { get; set; }

        [ForeignKey("JointTypeBID")]
        public JointType JointTypeB { get; set; }

        [ForeignKey("JointTypeCID")]
        public JointType JointTypeC { get; set; }
    }
}
