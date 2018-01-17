using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class VideoDTO : BaseDTO<VideoDTO>
    {
        public override int ID { get; set; }

        public byte[] Data { get; set; }
    }
}
