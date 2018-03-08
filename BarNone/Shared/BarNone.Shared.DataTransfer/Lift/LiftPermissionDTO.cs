using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class LiftPermissionDTO : BaseDTO<LiftPermissionDTO>
    {
        public override int ID { get; set; }

        public int UserID { get; set; }

        public int LiftID { get; set; }
    }
}
