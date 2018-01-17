using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverter.Lift;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    public class Converters : BaseConverter<Converters>
    {

        #region Converter(s).
        public UserConverter User { get; private set; }

        public LiftConverter Lift { get; private set; }

        public LiftFolderConverter LiftFolder { get; private set; }

        public BodyDataConverter BodyData { get; private set; }

        public BodyDataFrameConverter BodyDataFrame { get; private set; }

        public JointConverter Joint { get; private set; }

        public JointTypeConverter JointType { get; private set; }

        public JointTrackingStateTypeConverter JointTrackingStateType { get; private set; }

        public VideoConverter Video { get; private set; }
        #endregion

        #region Protected BaseConverter Implementation.
        protected override void Init(IDomainContext context)
        {
            /// TODO - pls no.
            User =                      Register<User,          UserDTO, UserConverter>(new UserConverter(this, context));
            Lift =                      Register<Lift,          LiftDTO, LiftConverter>(new LiftConverter(this, context));
            LiftFolder =                Register<LiftFolder,    LiftFolderDTO, LiftFolderConverter>(new LiftFolderConverter(this, context));
            BodyData =                  Register<BodyData,      BodyDataDTO, BodyDataConverter>(new BodyDataConverter(this, context));
            BodyDataFrame =             Register<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameConverter>(new BodyDataFrameConverter(this, context));
            Joint =                     Register<Joint,         JointDTO, JointConverter>(new JointConverter(this, context));
            JointType =                 Register<JointType,     JointTypeDTO, JointTypeConverter>(new JointTypeConverter(this, context));
            JointTrackingStateType =    Register<JointTrackingStateType, JointTrackingStateTypeDTO, JointTrackingStateTypeConverter>(new JointTrackingStateTypeConverter(this, context));
            Video =                     Register<VideoRecord,   VideoDTO, VideoConverter>(new VideoConverter(this, context));
            //Joint = Register<User, UserDTO, UserConverter>(new UserConverter(this));
        }
        #endregion
    }
}
