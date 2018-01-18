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
    public class JointTrackingStateTypeConverter
        : BaseEnumDataConverter<JointTrackingStateType, JointTrackingStateTypeDTO, EJointTrackingStateType>
    {
        public JointTrackingStateTypeConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }
    }
}
