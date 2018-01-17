using BarNone.Shared.Core;
using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Flex
{
    public class FlexDTO : BaseDTO<FlexDTO>
    {
        public override int ID { get; set; }

        public List<FlexEntityDTO> Entities { get; set; }
    }

    public class FlexEntityDTO
    {
        public string Resource { get; set; }

        public object Entity { get; set; }
    }
}
