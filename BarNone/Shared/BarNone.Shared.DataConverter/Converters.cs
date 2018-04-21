using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
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
        /// <summary>
        /// User Converter.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserConverter User { get; private set; }

        /// <summary>
        /// Comment Converter.
        /// </summary>
        /// <value>
        /// The comment converter.
        /// </value>
        public CommentConverter Comment { get; private set; }

        /// <summary>
        /// Lift Converter.
        /// </summary>
        /// <value>
        /// The lift.
        /// </value>
        public LiftPermissionConverter LiftPermission { get; private set; }

        /// <summary>
        /// Lift Type Converter.
        /// </summary>
        /// <value>
        /// The lift type.
        /// </value>
        public LiftTypeConverter LiftType { get; private set; }

        /// <summary>
        /// Lift Analysis Profile Converter.
        /// </summary>
        /// <value>
        /// The lift analysis profile.
        /// </value>
        public LiftAnalysisProfileConverter LiftAnalysisProfile { get; private set; }

        /// <summary>
        /// Lift Analysis Profile Converter.
        /// </summary>
        /// <value>
        /// The lift analysis profile.
        /// </value>
        public AccelerationAnalysisCriteriaConverter AccelerationAnalysisCriteria { get; private set; }

        /// <summary>
        /// Speed Analysis Criteria Converter.
        /// </summary>
        /// <value>
        /// The lift analysis profile.
        /// </value>
        public SpeedAnalysisCriteriaConverter SpeedAnalysisCriteria { get; private set; }

        /// <summary>
        /// Position Analysis Criteria Converter.
        /// </summary>
        /// <value>
        /// The lift analysis profile.
        /// </value>
        public PositionAnalysisCriteriaConverter PositionAnalysisCriteria { get; private set; }

        /// <summary>
        /// Angle Analysis Criteria Converter.
        /// </summary>
        /// <value>
        /// The lift analysis profile.
        /// </value>
        public AngleAnalysisCriteriaConverter AngleAnalysisCriteria { get; private set; }

        /// <summary>
        /// Lift Converter.
        /// </summary>
        /// <value>
        /// The lift.
        /// </value>
        public LiftConverter Lift { get; private set; }

        /// <summary>
        /// LiftFolder Converter.
        /// </summary>
        /// <value>
        /// The lift folder.
        /// </value>
        public LiftFolderConverter LiftFolder { get; private set; }

        /// <summary>
        /// Body Converter.
        /// </summary>
        /// <value>
        /// The body data.
        /// </value>
        public BodyDataConverter BodyData { get; private set; }

        /// <summary>
        /// BodyDataFrame Converter.
        /// </summary>
        /// <value>
        /// The body data frame.
        /// </value>
        public BodyDataFrameConverter BodyDataFrame { get; private set; }

        /// <summary>
        /// Joint Converter.
        /// </summary>
        /// <value>
        /// The joint.
        /// </value>
        public JointConverter Joint { get; private set; }

        /// <summary>
        /// Joint Converter.
        /// </summary>
        /// <value>
        /// The type of the joint.
        /// </value>
        public JointTypeConverter JointType { get; private set; }

        /// <summary>
        /// JointTrackingState Converter.
        /// </summary>
        /// <value>
        /// The type of the joint tracking state.
        /// </value>
        public JointTrackingStateTypeConverter JointTrackingStateType { get; private set; }

        /// <summary>
        /// Video converter.
        /// </summary>
        /// <value>
        /// The video.
        /// </value>
        public VideoConverter Video { get; private set; }

        public NotificationConverter Notification { get; private set; }
        #endregion

        #region Protected BaseConverter Implementation.        
        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Init(IDomainContext context)
        {
            /// TODO - pls no.
            User =                      Register<User,          UserDTO, UserConverter>(new UserConverter(this, context));
            Comment =                   Register<Comment,       CommentDTO, CommentConverter>(new CommentConverter(this, context));
            LiftPermission =            Register<LiftPermission, LiftPermissionDTO, LiftPermissionConverter>(new LiftPermissionConverter(this, context));
            LiftType =                  Register<LiftType, LiftTypeDTO, LiftTypeConverter>(new LiftTypeConverter(this, context));
            LiftAnalysisProfile =       Register<LiftAnalysisProfile, LiftAnalysisProfileDTO, LiftAnalysisProfileConverter>(new LiftAnalysisProfileConverter(this, context));
            AccelerationAnalysisCriteria = Register<AccelerationAnalysisCriteria, AccelerationAnalysisCriteriaDTO, AccelerationAnalysisCriteriaConverter>(new AccelerationAnalysisCriteriaConverter(this, context));

            PositionAnalysisCriteria = Register<PositionAnalysisCriteria, PositionAnalysisCriteriaDTO, PositionAnalysisCriteriaConverter>(new PositionAnalysisCriteriaConverter(this, context));
            SpeedAnalysisCriteria = Register<SpeedAnalysisCriteria, SpeedAnalysisCriteriaDTO, SpeedAnalysisCriteriaConverter>(new SpeedAnalysisCriteriaConverter(this, context));
            AngleAnalysisCriteria = Register<AngleAnalysisCriteria, AngleAnalysisCriteriaDTO, AngleAnalysisCriteriaConverter>(new AngleAnalysisCriteriaConverter(this, context));

            Lift =                      Register<Lift,          LiftDTO, LiftConverter>(new LiftConverter(this, context));
            LiftFolder =                Register<LiftFolder,    LiftFolderDTO, LiftFolderConverter>(new LiftFolderConverter(this, context));
            BodyData =                  Register<BodyData,      BodyDataDTO, BodyDataConverter>(new BodyDataConverter(this, context));
            BodyDataFrame =             Register<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameConverter>(new BodyDataFrameConverter(this, context));
            Joint =                     Register<Joint,         JointDTO, JointConverter>(new JointConverter(this, context));
            JointType =                 Register<JointType,     JointTypeDTO, JointTypeConverter>(new JointTypeConverter(this, context));
            JointTrackingStateType =    Register<JointTrackingStateType, JointTrackingStateTypeDTO, JointTrackingStateTypeConverter>(new JointTrackingStateTypeConverter(this, context));
            Video =                     Register<VideoRecord,   VideoDTO, VideoConverter>(new VideoConverter(this, context));
            Notification =              Register<Notification,  NotificationDTO, NotificationConverter>(new NotificationConverter(this, context));
            //Joint = Register<User, UserDTO, UserConverter>(new UserConverter(this));
        }
        #endregion
    }
}
