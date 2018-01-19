using BarNone.Shared.DomainModel.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Lift domain model
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{BarNone.Shared.DomainModel.LiftFolder}" />
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IOwnedDomainModel" />
    [Table("LiftFolder", Schema = "public")]
    public class LiftFolder : IDomainModel<LiftFolder>, IOwnedDomainModel
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
        /// Gets or sets the lifts.
        /// </summary>
        /// <value>
        /// The lifts.
        /// </value>
        public List<Lift> Lifts { get; set; }

        /// <summary>
        /// Gets or sets the sub folders.
        /// </summary>
        /// <value>
        /// The sub folders.
        /// </value>
        public List<LiftFolder> SubFolders { get; set; }
    }
}
