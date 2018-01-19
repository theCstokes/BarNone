//using BarNone.DataLift.DataConverters;
//using BarNone.DataLift.DataModel.KinectData;
//using BarNone.Shared.DataTransfer;
//using BarNone.Shared.DataTransfer.LiftData;
//
//using Microsoft.Kinect;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;

//namespace BarNone.UnitTest.DataLift.DomainModel
//{
//    [TestClass]
//    public class BodyData_UnitTest
//    {
//        [TestMethod]
//        public void BuildBodyData_Test()
//        {
//            var bodyData = new BodyData();
//            var bodyDataDTO = Converters.Convert.BodyData.CreateDTO(bodyData); //.CreateDTO(new ConvertConfig());

//            Assert.IsNotNull(bodyDataDTO);
//        }

//        [TestMethod]
//        public void BodyDataDTOProperties_Test()
//        {
//            var bodyData = new BodyData()
//            {
//                RecordDate = DateTime.Now,
//                DataFrames = null
//            };

//            var bodyDataDTO = Converters.Convert.BodyData.CreateDTO(bodyData);

//            Assert.AreEqual(bodyData.RecordDate, bodyDataDTO.RecordTimeStamp);
//        }
//        [TestMethod]
//        public void BodyDataDTODetailProp_Test()
//        {
//            var bodyData = new BodyData()
//            {
//                RecordDate = DateTime.Now,
//                DataFrames = new List<BodyDataFrame>()
//                {
//                    new BodyDataFrame() {TimeOfFrame = DateTime.Now, Joints = new Dictionary<JointType, Joint>()
//                    {
//                        {(JointType)0, new Joint {JointType = (JointType)0, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } },
//                        {(JointType)1, new Joint {JointType = (JointType)1, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } }
//                    } }, new BodyDataFrame() {TimeOfFrame = DateTime.Now, Joints = new Dictionary<JointType, Joint>()
//                    {
//                        {(JointType)0, new Joint {JointType = (JointType)0, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } },
//                        {(JointType)1, new Joint {JointType = (JointType)1, Position = new CameraSpacePoint {X = 111, Y = 222, Z = 333}, TrackingState = (TrackingState)2 } }
//                    } }
//                }
//            };

//            var bodyDataDTO = Converters.Convert.BodyData.CreateDTO(bodyData);

//            Assert.AreEqual(bodyData.RecordDate, bodyDataDTO.RecordTimeStamp);

//            for (int i = 0; i < bodyData.DataFrames.Count; i++)
//            {
//                var frame = bodyData.DataFrames[i];
//                var frameDTO = bodyDataDTO.Details.OrderedFrames[i];
                
//                Assert.AreEqual(frame.TimeOfFrame, frameDTO.TimeOfFrame);
//            }
//        }

//        [TestMethod]
//        public void BodyDataDMProperties_Test()
//        {
//            var bodyDataDTO = new BodyDataDTO()
//            {
//                ID = 1,
//                RecordTimeStamp = DateTime.Now,
//                Details = null
//            };

//            var bodyData = Converters.Convert.BodyData.CreateDataModel(bodyDataDTO);

//            Assert.AreEqual(bodyDataDTO.RecordTimeStamp, bodyData.RecordDate);
//        }

//        [TestMethod]
//        public void BodyDataDMDetailProp_Test()
//        {
//            var bodyDataDTO = new BodyDataDTO()
//            {
//                ID = 1,
//                RecordTimeStamp = DateTime.Now,
//                Details = new BodyDataDetailDTO()
//                {
//                    OrderedFrames = new List<BodyDataFrameDTO>()
//                    {
//                        new BodyDataFrameDTO()
//                        {
//                            ID = 1,
//                            TimeOfFrame = DateTime.Now,
//                            Details = new BodyDataFrameDetailDTO()
//                            {
//                                Joints = new List<JointDTO>()
//                                {
//                                    { new JointDTO {JointType = new JointTypeDTO() { Value = 0}, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = new TrackingStateDTO() { Value = 2} } },
//                                    { new JointDTO {JointType = new JointTypeDTO() { Value = 1}, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = new TrackingStateDTO() { Value = 2} } }
//                                }
//                            }
//                        },
//                        new BodyDataFrameDTO()
//                        {
//                            ID = 2,
//                            TimeOfFrame = DateTime.Now,
//                            Details = new BodyDataFrameDetailDTO()
//                            {
//                                Joints = new List<JointDTO>()
//                                {
//                                    { new JointDTO {JointType = new JointTypeDTO() { Value = 0}, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = new TrackingStateDTO() { Value = 2} } },
//                                    { new JointDTO {JointType = new JointTypeDTO() { Value = 1}, PositionX = 111, PositionY = 222, PositionZ = 333, TrackingState = new TrackingStateDTO() { Value = 2} } }
//                                }
//                            }
//                        },
//                    }
//                }
//            };

//            var bodyData = Converters.Convert.BodyData.CreateDataModel(bodyDataDTO);
            
//            Assert.AreEqual(bodyData.RecordDate, bodyDataDTO.RecordTimeStamp);

//            for (int i = 0; i < bodyData.DataFrames.Count; i++)
//            {
//                var frame = bodyData.DataFrames[i];
//                var frameDTO = bodyDataDTO.Details.OrderedFrames[i];
                
//                Assert.AreEqual(frame.TimeOfFrame, frameDTO.TimeOfFrame);
//            }
//        }
//    }
//}
