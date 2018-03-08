using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class LiftTypeDTO : BaseDTO<LiftTypeDTO>
    {
        public override int ID { get; set; }

        public string Name { get; set; }
    }
}
