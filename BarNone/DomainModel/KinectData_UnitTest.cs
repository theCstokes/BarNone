using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarNone.DataLift.DomainModel.KinectData;
using System;
using Microsoft.Kinect;
using System.Collections.Generic;
using BarNone.Shared.DataTransfer.Types;

namespace BarNone.UnitTest.DataLift.DomainModel
{
    [TestClass]
    public class KinectData_UnitTest
    {
        [TestMethod]
        public void BuildBDFDTO()
        {
            var bdf = new BodyDataFrame();
            var bdfDTO = bdf.BuildDTO();

            Assert.IsNotNull(bdfDTO);
        }

        [TestMethod]
        public void CheckBDFDTOProperties()
        {
            var bdf = new BodyDataFrame()
            {
                ID = 1,
                TimeOfFrame = DateTime.Now,

                Joints = new Dictionary<JointType, Joint>()
                {
                    {(JointType)0, new Joint { JointType = 0, Position = new CameraSpacePoint{ X = 111, Y = 222, Z =333 }, TrackingState = (TrackingState)2 } },
                    {(JointType)2, new Joint { JointType = (JointType)2, Position = new CameraSpacePoint{ X = 111, Y = 222, Z =333 }, TrackingState = (TrackingState)2 } },
                }
            };

            var bdfDTO = bdf.BuildDTO();

            Assert.IsNotNull(bdfDTO);
            Assert.AreEqual(bdf.ID, bdfDTO.ID);
            Assert.AreEqual(bdf.TimeOfFrame, bdfDTO.TimeOfFrame);
            
            foreach(KeyValuePair<JointType, Joint> entry in bdf.Joints)
            {
                var DTOentry = bdfDTO.Details.Joints[(DTOJointType)entry.Key];

                Assert.AreEqual(entry.Value.JointType, (JointType)DTOentry.JointType);
                Assert.AreEqual(entry.Value.TrackingState, (TrackingState)DTOentry.TrackingState);
                Assert.AreEqual(entry.Value.Position.X, DTOentry.PositionX);
                Assert.AreEqual(entry.Value.Position.Y, DTOentry.PositionY);
                Assert.AreEqual(entry.Value.Position.Z, DTOentry.PositionZ);
            }

        }
    }
}
