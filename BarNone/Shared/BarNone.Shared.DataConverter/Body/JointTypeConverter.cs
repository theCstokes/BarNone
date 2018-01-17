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
    public class JointTypeConverter
        : BaseEnumDataConverter<JointType, JointTypeDTO, EJointType>
    {
        public JointTypeConverter(Converters converterContext) : base(converterContext)
        {
        }
    }
}
