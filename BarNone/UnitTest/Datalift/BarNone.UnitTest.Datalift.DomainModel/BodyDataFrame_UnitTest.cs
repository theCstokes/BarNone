using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.LiftData;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarNone.UnitTest.DataLift.DomainModel
{
    [TestClass]
    public class BodyDataFrame_UnitTest
    {
        [TestMethod]
        public void BuildDTO_Test()
        {
            var dataFrame = new BodyDataFrame();
            var dataFrameDTO = dataFrame.CreateDTO();

            Assert.IsNotNull(dataFrameDTO);
        }

        [TestMethod]
        public void DTOProperties_Test()
        {
            var dataFrame = new BodyDataFrame()
            {
                TimeOfFrame = DateTime.Now
            };

            var dataFrameDTO = dataFrame.CreateDTO();
            Assert.AreEqual(dataFrame.TimeOfFrame, dataFrameDTO.TimeOfFrame);
        }
        
        [TestMethod]
        public void DTODetailproperties_Test()
        {
            var dataFame = new BodyDataFrame()
            {
                TimeOfFrame = DateTime.Now,
                Joints = new Dictionary<JointType, Joint>()
                {
                    {(JointType)0, new Joint {JointType = (JointType)0, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } },
                    {(JointType)1, new Joint {JointType = (JointType)1, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } }
                }
            };

            var dataFameDTO = dataFame.CreateDTO();

            foreach (KeyValuePair<JointType,Joint> joint in dataFame.Joints)
            {
                
                var jointDTO = dataFameDTO.Details.Joints.FirstOrDefault(x => x.JointType.Value == (int)joint.Key);

                Assert.AreEqual(joint.Value.JointType, (JointType)Enum.ToObject(typeof(JointType) ,jointDTO.JointType.Value));
                Assert.AreEqual(joint.Value.TrackingState, (TrackingState)Enum.ToObject(typeof(JointType), jointDTO.TrackingState.Value));

                Assert.AreEqual(joint.Value.Position.X, jointDTO.X);
                Assert.AreEqual(joint.Value.Position.Y, jointDTO.Y);
                Assert.AreEqual(joint.Value.Position.Z, jointDTO.Z);
            }
        }

        [TestMethod]
        public void DMProperties_Test()
        {
            var dataFrameDTO = new BodyDataFrameDTO()
            {
                ID = 1,
                TimeOfFrame = DateTime.Now,
            };

            var dataFrame = BodyDataFrame.CreateFromDTO(dataFrameDTO, new Shared.DTOTransformable.Core.ConvertConfig() { Infinite = true});
            
            Assert.AreEqual(dataFrameDTO.TimeOfFrame, dataFrame.TimeOfFrame);
        }

        [TestMethod]
        public void DMDetailProperties_Test()
        {
            var dataFrameDTO = new BodyDataFrameDTO()
            {
                ID = 1,
                TimeOfFrame = DateTime.Now,
                Details = new BodyDataFrameDetailDTO()
                {
                    Joints = new List<JointDTO>()
                    {
<<<<<<< HEAD
                        {(DTOJointType)0, new JointDTO {JointType = (DTOJointType)0, X = 111, Y = 222, Z = 333, JointTrackingStateType = (DTOTrackingState)2} },
                        {(DTOJointType)1, new JointDTO {JointType = (DTOJointType)1, X = 111, Y = 222, Z = 333, JointTrackingStateType = (DTOTrackingState)2} }
=======
                        { new JointDTO {JointType = new JointTypeDTO() { Value = 0}, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = new TrackingStateDTO() { Value = 2} } },
                        {new JointDTO {JointType = new JointTypeDTO() { Value = 1}, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = new TrackingStateDTO() { Value = 2} } }
>>>>>>> develop
                    }
                }
            };

            var dataFrame = BodyDataFrame.CreateFromDTO(dataFrameDTO);

            foreach(JointDTO jointDTO in dataFrameDTO.Details.Joints)
            {
                var joint = dataFrame.Joints[(JointType)jointDTO.JointType.Value];

<<<<<<< HEAD
                Assert.AreEqual(joint.JointType, (JointType)jointDTO.Value.JointType);
                Assert.AreEqual(joint.TrackingState, (TrackingState)jointDTO.Value.JointTrackingStateType);

                Assert.AreEqual(joint.Position.X, jointDTO.Value.X);
                Assert.AreEqual(joint.Position.Y, jointDTO.Value.Y);
                Assert.AreEqual(joint.Position.Z, jointDTO.Value.Z);
=======
                Assert.AreEqual(joint.JointType, (JointType)Enum.ToObject(typeof(JointType),jointDTO.JointType.Value));
                Assert.AreEqual(joint.TrackingState, (TrackingState)Enum.ToObject(typeof(TrackingState),jointDTO.TrackingState.Value));

                Assert.AreEqual(joint.Position.X, jointDTO.PositionX);
                Assert.AreEqual(joint.Position.Y, jointDTO.PositionY);
                Assert.AreEqual(joint.Position.Z, jointDTO.PositionZ);
>>>>>>> develop
            }
        }
    }
}
