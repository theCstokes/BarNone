using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Body domain model.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{BarNone.Shared.DomainModel.BodyData}" />
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IOwnedDomainModel" />
    [Table("BodyData", Schema = "public")]
    public class BodyData : IDomainModel<BodyData>, IOwnedDomainModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the record date.
        /// </summary>
        /// <value>
        /// The record date.
        /// </value>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// Gets or sets the body data frames.
        /// </summary>
        /// <value>
        /// The body data frames.
        /// </value>
        public List<BodyDataFrame> BodyDataFrames { get; set; }
    }
}
