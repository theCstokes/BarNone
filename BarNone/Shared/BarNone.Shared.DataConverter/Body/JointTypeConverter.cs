using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    /// <summary>
    /// Joint type converter
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverters.Core.BaseEnumDataConverter{BarNone.Shared.DomainModel.JointType, BarNone.Shared.DataTransfer.LiftData.JointTypeDTO, BarNone.Shared.DomainModel.EJointType}" />
    public class JointTypeConverter
        : BaseEnumDataConverter<JointType, JointTypeDTO, EJointType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JointTypeConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public JointTypeConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }
    }
}
