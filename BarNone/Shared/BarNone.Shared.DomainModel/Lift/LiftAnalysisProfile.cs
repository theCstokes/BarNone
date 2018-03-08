﻿using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("LiftAnalysisProfile", Schema = "public")]
    public class LiftAnalysisProfile : IDomainModel<LiftAnalysisProfile>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int LiftTypeID { get; set; }

        [ForeignKey("LiftTypeID")]
        public LiftType LiftType { get; set; }

        public List<AccelerationAnalysisProfile> AccelerationAnalysis { get; set; }
    }
}
