using BarNone.Shared.DomainModel.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Lift domain model.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{BarNone.Shared.DomainModel.Lift}" />
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IOwnedDomainModel" />
    [Table("Lift", Schema = "public")]
    public class Lift : IDomainModel<Lift>, IOwnedDomainModel
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public int? ParentID { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [ForeignKey("ParentID")]
        public LiftFolder Parent { get; set; }

        /// <summary>
        /// Gets or sets the body data identifier.
        /// </summary>
        /// <value>
        /// The body data identifier.
        /// </value>
        public int? BodyDataID { get; set; }

        /// <summary>
        /// Gets or sets the body data.
        /// </summary>
        /// <value>
        /// The body data.
        /// </value>
        public BodyData BodyData { get; set; }

        /// <summary>
        /// Gets or sets the video identifier.
        /// </summary>
        /// <value>
        /// The video identifier.
        /// </value>
        public int VideoID { get; set; }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        /// <value>
        /// The video.
        /// </value>
        [ForeignKey("VideoID")]
        public VideoRecord Video { get; set; }

        public List<LiftPermission> Permissions { get; set; }
    }
}
