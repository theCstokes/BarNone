using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarNone.DataLift.DomainModel.KinectData;
using System.Collections.Generic;
using BarNone.Shared.DataTransfer;
using Microsoft.Kinect;
using BarNone.Shared.DataTransfer.Types;

namespace BarNone.UnitTest.DataLift.DomainModel
{
    [TestClass]
    public class BodyData_UnitTest
    {
        [TestMethod]
        public void BuildBodyData_Test()
        {
            var bodyData = new BodyData();
            var bodyDataDTO = bodyData.BuildDTO();

            Assert.IsNotNull(bodyDataDTO);
        }

        [TestMethod]
        public void BodyDataDTOProperties_Test()
        {
            var bodyData = new BodyData()
            {
                ID = 1,
                RecordDate = DateTime.Now,
                DataFrames = null
            };

            var bodyDataDTO = bodyData.BuildDTO();

            Assert.AreEqual(bodyData.ID, bodyDataDTO.ID);
            Assert.AreEqual(bodyData.RecordDate, bodyDataDTO.RecordTimeStamp);
        }
        [TestMethod]
        public void BodyDataDTODetailProp_Test()
        {
            var bodyData = new BodyData()
            {
                ID = 1,
                RecordDate = DateTime.Now,
                DataFrames = new List<BodyDataFrame>()
                {
                    new BodyDataFrame() {ID = 1, TimeOfFrame = DateTime.Now, Joints = new Dictionary<JointType, Joint>()
                    {
                        {(JointType)0, new Joint {JointType = (JointType)0, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } },
                        {(JointType)1, new Joint {JointType = (JointType)1, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } }
                    } }, new BodyDataFrame() {ID = 2, TimeOfFrame = DateTime.Now, Joints = new Dictionary<JointType, Joint>()
                    {
                        {(JointType)0, new Joint {JointType = (JointType)0, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } },
                        {(JointType)1, new Joint {JointType = (JointType)1, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } }
                    } }
                }
            };

            var bodyDataDTO = bodyData.BuildDTO();
            bodyDataDTO.Details = bodyData.BuildDetailDTO();
            //bodyDataDTO.Details.OrderedFrames


            Assert.AreEqual(bodyData.ID, bodyDataDTO.ID);
            Assert.AreEqual(bodyData.RecordDate, bodyDataDTO.RecordTimeStamp);

            for(int i = 0; i < bodyData.DataFrames.Count; i++)
            {
                var frame = bodyData.DataFrames[i];
                var frameDTO = bodyDataDTO.Details.OrderedFrames[i];

                Assert.AreEqual(frame.ID, frameDTO.ID);
                Assert.AreEqual(frame.TimeOfFrame, frameDTO.TimeOfFrame);
            }
        }

        [TestMethod]
        public void BodyDataDMProperties_Test()
        {
            var bodyDataDTO = new BodyDataDTO()
            {
                ID = 1,
                RecordTimeStamp = DateTime.Now,
                Details = null
            };

            var bodyData = BodyData.CreateFromDTO(bodyDataDTO);

            Assert.AreEqual(bodyDataDTO.ID, bodyData.ID);
            Assert.AreEqual(bodyDataDTO.RecordTimeStamp, bodyData.RecordDate);
        }

        [TestMethod]
        public void BodyDataDMDetailProp_Test()
        {
            var bodyDataDTO = new BodyDataDTO()
            {
                ID = 1,
                RecordTimeStamp = DateTime.Now,
                Details = new BodyDataDetailDTO()
                {
                    OrderedFrames = new List<BodyDataFrameDTO>()
                    {
                        new BodyDataFrameDTO()
                        {
                            ID = 1,
                            TimeOfFrame = DateTime.Now,
                            Details = new BodyDataFrameDetailDTO()
                            {
                                Joints = new Dictionary<DTOJointType, JointDTO>()
                                {
                                    {(DTOJointType)0, new JointDTO {JointType = (DTOJointType)0, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = (DTOTrackingState)2} },
                                    {(DTOJointType)1, new JointDTO {JointType = (DTOJointType)1, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = (DTOTrackingState)2} }
                                }
                            }
                        },
                        new BodyDataFrameDTO()
                        {
                            ID = 2,
                            TimeOfFrame = DateTime.Now,
                            Details = new BodyDataFrameDetailDTO()
                            {
                                Joints = new Dictionary<DTOJointType, JointDTO>()
                                {
                                    {(DTOJointType)0, new JointDTO {JointType = (DTOJointType)0, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = (DTOTrackingState)2} },
                                    {(DTOJointType)1, new JointDTO {JointType = (DTOJointType)1, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = (DTOTrackingState)2} }
                                }
                            }
                        },
                    }
                }
            };

            var bodyData = BodyData.CreateFromDTO(bodyDataDTO);

            Assert.AreEqual(bodyData.ID, bodyDataDTO.ID);
            Assert.AreEqual(bodyData.RecordDate, bodyDataDTO.RecordTimeStamp);

            for (int i = 0; i < bodyData.DataFrames.Count; i++)
            {
                var frame = bodyData.DataFrames[i];
                var frameDTO = bodyDataDTO.Details.OrderedFrames[i];

                Assert.AreEqual(frame.ID, frameDTO.ID);
                Assert.AreEqual(frame.TimeOfFrame, frameDTO.TimeOfFrame);
            }
        }
    }
}
