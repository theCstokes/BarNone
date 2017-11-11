using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.LiftData
{
    public class JointTypeDTO : BaseDTO<JointTypeDTO>
    {
        #region Public Property(s).
        public override int ID { get; set; }

        public int Value { get; set; }

        public string Name { get; set; }
        #endregion
    }
}
