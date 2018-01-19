using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverters.Core;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    /// <summary>
    /// Joint tracking state type converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverters.Core.BaseEnumDataConverter{BarNone.Shared.DomainModel.JointTrackingStateType, BarNone.Shared.DataTransfer.LiftData.JointTrackingStateTypeDTO, BarNone.Shared.DomainModel.EJointTrackingStateType}" />
    public class JointTrackingStateTypeConverter
        : BaseEnumDataConverter<JointTrackingStateType, JointTrackingStateTypeDTO, EJointTrackingStateType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JointTrackingStateTypeConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public JointTrackingStateTypeConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }
    }
}
