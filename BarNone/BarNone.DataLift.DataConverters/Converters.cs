﻿using BarNone.DataLift.DataConverters.KinectData;
using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.DataConverters
{
    public class Converters : BaseConverter<Converters>
    {
        #region Converter(s).
        public BodyDataConverter BodyData { get; private set; }
        public BodyDataFrameConverter BodyDataFrame { get; private set; }
        #endregion
        protected override void Init()
        {
            BodyData = Register<BodyData, BodyDataDTO, BodyDataConverter>(new BodyDataConverter(this));
            BodyDataFrame = Register<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameConverter>(new BodyDataFrameConverter(this));
        }
    }
}