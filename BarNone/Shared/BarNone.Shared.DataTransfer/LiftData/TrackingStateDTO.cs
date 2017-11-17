using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.LiftData
{
    public class TrackingStateDTO : BaseTypeDTO<TrackingStateDTO>
    {
        #region Public Property(s).
        public override int ID { get; set; }

        public override int Value { get; set; }

        public override string Name { get; set; }
        #endregion
    }
}
