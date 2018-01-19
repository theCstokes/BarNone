using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// Video dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDTO{BarNone.Shared.DataTransfer.VideoDTO}" />
    public class VideoDTO : BaseDTO<VideoDTO>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public byte[] Data { get; set; }
    }
}
