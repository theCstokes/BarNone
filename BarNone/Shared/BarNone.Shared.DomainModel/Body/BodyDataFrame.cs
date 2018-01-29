using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Body data frame domain model.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{BarNone.Shared.DomainModel.BodyDataFrame}" />
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IOwnedDomainModel" />
    [Table("BodyDataFrame", Schema = "public")]
    public class BodyDataFrame : IDomainModel<BodyDataFrame>, IOwnedDomainModel
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
        /// Gets or sets the time of frame.
        /// </summary>
        /// <value>
        /// The time of frame.
        /// </value>
        public TimeSpan TimeOfFrame { get; set; }

        /// <summary>
        /// Gets or sets the joints.
        /// </summary>
        /// <value>
        /// The joints.
        /// </value>
        public List<Joint> Joints { get; set; }

        /// <summary>
        /// Gets or sets the body data identifier.
        /// </summary>
        /// <value>
        /// The body data identifier.
        /// </value>
        [ForeignKey("BodyDataID")]
        public int BodyDataID { get; set; }

        /// <summary>
        /// Gets or sets the body data.
        /// </summary>
        /// <value>
        /// The body data.
        /// </value>
        public BodyData BodyData { get; set; }
    }
}
