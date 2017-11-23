using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.TheRack.DataConverters.Core;
using BarNone.TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataConverters.Body
{
    public class JointTypeConverter
        : BaseEnumDataConverter<JointType, JointTypeDTO, EJointType>
    {
        public JointTypeConverter(Converters converterContext) : base(converterContext)
        {
        }
    }
}
