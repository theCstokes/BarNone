using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Video domain model.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{BarNone.Shared.DomainModel.VideoRecord}" />
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IOwnedDomainModel" />
    [Table("Video", Schema = "public")]
    public class VideoRecord : IDomainModel<VideoRecord>, IOwnedDomainModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [NotMapped]
        public byte[] Data { get; set; }
    }
}
