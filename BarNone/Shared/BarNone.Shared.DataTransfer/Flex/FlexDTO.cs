using BarNone.Shared.Core;
using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Flex
{
    /// <summary>
    /// Flex dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDTO{BarNone.Shared.DataTransfer.Flex.FlexDTO}" />
    public class FlexDTO : BaseDTO<FlexDTO>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public List<FlexEntityDTO> Entities { get; set; }
    }

    /// <summary>
    /// Flex entity dto.
    /// </summary>
    public class FlexEntityDTO
    {
        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>
        /// The resource.
        /// </value>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public object Entity { get; set; }
    }
}
