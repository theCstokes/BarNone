using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.TheRack.DataConverters.Core;
using BarNone.TheRack.DomainModel.Body;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataConverters.Body
{
    public class JointTrackingStateTypeConverter
        : BaseEnumDataConverter<JointTrackingStateType, JointTrackingStateTypeDTO, EJointTrackingStateType>
    {
        public JointTrackingStateTypeConverter(Converters converterContext) : base(converterContext)
        {
        }
    }
}
