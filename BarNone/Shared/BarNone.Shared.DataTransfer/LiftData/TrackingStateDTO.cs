using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DataTransfer.LiftData
{
    public class JointTrackingStateTypeDTO : BaseTypeDTO<JointTrackingStateTypeDTO>
    {
        #region Public Property(s).        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public override int Value { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name { get; set; }
        #endregion
    }
}
