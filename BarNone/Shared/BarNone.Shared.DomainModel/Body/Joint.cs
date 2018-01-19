using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarNone.Shared.DomainModel
{
    /// <summary>
    /// Joint domain model.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IDomainModel{BarNone.Shared.DomainModel.Joint}" />
    /// <seealso cref="BarNone.Shared.DomainModel.Core.IOwnedDomainModel" />
    [Table("Joint", Schema = "public")]
    public class Joint : IDomainModel<Joint>, IOwnedDomainModel
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
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        /// <value>
        /// The z.
        /// </value>
        public float Z { get; set; }

        /// <summary>
        /// Gets or sets the body data frame identifier.
        /// </summary>
        /// <value>
        /// The body data frame identifier.
        /// </value>
        public int BodyDataFrameID { get; set; }

        /// <summary>
        /// Gets or sets the body data frame.
        /// </summary>
        /// <value>
        /// The body data frame.
        /// </value>
        [ForeignKey("BodyDataFrameID")]
        public BodyDataFrame BodyDataFrame { get; set; }

        /// <summary>
        /// Gets or sets the joint type identifier.
        /// </summary>
        /// <value>
        /// The joint type identifier.
        /// </value>
        public int JointTypeID { get; set; }

        /// <summary>
        /// Gets or sets the type of the joint.
        /// </summary>
        /// <value>
        /// The type of the joint.
        /// </value>
        [ForeignKey("JointTypeID")]
        public JointType JointType { get; set; }

        /// <summary>
        /// Gets or sets the joint tracking state type identifier.
        /// </summary>
        /// <value>
        /// The joint tracking state type identifier.
        /// </value>
        public int JointTrackingStateTypeID { get; set; }

        /// <summary>
        /// Gets or sets the type of the joint tracking state.
        /// </summary>
        /// <value>
        /// The type of the joint tracking state.
        /// </value>
        [ForeignKey("JointTrackingStateTypeID")]
        public JointTrackingStateType JointTrackingStateType { get; set; }
    }
}
