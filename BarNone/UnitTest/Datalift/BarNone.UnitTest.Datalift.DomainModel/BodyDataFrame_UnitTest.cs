using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarNone.DataLift.DataModel.KinectData;
using System.Collections.Generic;
using Microsoft.Kinect;
using BarNone.Shared.DataTransfer.Types;
using BarNone.Shared.DataTransfer;

namespace BarNone.UnitTest.DataLift.DomainModel
{
    [TestClass]
    public class BodyDataFrame_UnitTest
    {
        [TestMethod]
        public void BuildDTO_Test()
        {
            var dataFrame = new BodyDataFrame();
            var dataFrameDTO = dataFrame.BuildDTO();

            Assert.IsNotNull(dataFrameDTO);
        }

        [TestMethod]
        public void DTOProperties_Test()
        {
            var dataFrame = new BodyDataFrame()
            {
                ID = 1,
                TimeOfFrame = DateTime.Now
            };

            var dataFrameDTO = dataFrame.BuildDTO();

            Assert.AreEqual(dataFrame.ID, dataFrameDTO.ID);
            Assert.AreEqual(dataFrame.TimeOfFrame, dataFrameDTO.TimeOfFrame);
        }
        
        [TestMethod]
        public void DTODetailproperties_Test()
        {
            var dataFame = new BodyDataFrame()
            {
                ID = 1,
                TimeOfFrame = DateTime.Now,
                Joints = new Dictionary<JointType, Joint>()
                {
                    {(JointType)0, new Joint {JointType = (JointType)0, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } },
                    {(JointType)1, new Joint {JointType = (JointType)1, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } }
                }
            };

            var dataFameDTO = dataFame.BuildDTO();
            dataFameDTO.Details = dataFame.CreateDTO();

            foreach (KeyValuePair<JointType,Joint> joint in dataFame.Joints)
            {
                var jointDTO = dataFameDTO.Details.Joints[(DTOJointType)joint.Key];

                Assert.AreEqual(joint.Value.JointType, (JointType)jointDTO.JointType);
                Assert.AreEqual(joint.Value.TrackingState, (TrackingState)jointDTO.JointTrackingStateType);

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

            var dataFrame = BodyDataFrame.CreateFromDTO(dataFrameDTO);

            Assert.AreEqual(dataFrameDTO.ID, dataFrame.ID);
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
                    Joints = new Dictionary<DTOJointType, JointDTO>()
                    {
                        {(DTOJointType)0, new JointDTO {JointType = (DTOJointType)0, X = 111, Y = 222, Z = 333, JointTrackingStateType = (DTOTrackingState)2} },
                        {(DTOJointType)1, new JointDTO {JointType = (DTOJointType)1, X = 111, Y = 222, Z = 333, JointTrackingStateType = (DTOTrackingState)2} }
                    }
                }
            };

            var dataFrame = BodyDataFrame.CreateFromDTO(dataFrameDTO);

            foreach(KeyValuePair<DTOJointType, JointDTO> jointDTO in dataFrameDTO.Details.Joints)
            {
                var joint = dataFrame.Joints[(JointType)jointDTO.Key];

                Assert.AreEqual(joint.JointType, (JointType)jointDTO.Value.JointType);
                Assert.AreEqual(joint.TrackingState, (TrackingState)jointDTO.Value.JointTrackingStateType);

                Assert.AreEqual(joint.Position.X, jointDTO.Value.X);
                Assert.AreEqual(joint.Position.Y, jointDTO.Value.Y);
                Assert.AreEqual(joint.Position.Z, jointDTO.Value.Z);
            }
        }
    }
}
