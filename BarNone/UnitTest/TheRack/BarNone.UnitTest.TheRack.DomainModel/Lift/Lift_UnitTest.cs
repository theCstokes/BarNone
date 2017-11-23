using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BarNone.UnitTest.TheRack.DomainModel
{
    [TestClass]
    public class Lift_UnitTest
    {
        [TestMethod]
        public void BuildDTO_Create_Test()
        {
            var lift = new Lift();
            var liftDTO = lift.CreateDTO(new ConvertConfig());
            Assert.IsNotNull(liftDTO);
        }

        [TestMethod]
        public void BuildDTO_Properties_Test()
        {
            var lift = new Lift
            {
                ID = 1,
                Name = "Test",
                ParentID = 44
            };
            var liftDTO = lift.CreateDTO(new ConvertConfig());
            Assert.IsNotNull(liftDTO);
            Assert.AreEqual(lift.ID, liftDTO.ID);
            Assert.AreEqual(lift.Name, liftDTO.Name);
            Assert.AreEqual(lift.ParentID, liftDTO.ParentID);
        }

        [TestMethod]
        public void BuildDTO_Details_Test()
        {
            var lift = new Lift
            {
                ID = 1,
                Name = "Test",
                ParentID = 44
            };
            var parent = new LiftFolder
            {
                ID = 44,
                Name = "TestFolder",
                Lifts = new List<Lift> { lift }
            };
            lift.Parent = parent;

            var parentDTO = parent.CreateDTO(new ConvertConfig());

            var config = new ConvertConfig(parentDTO);

            var liftDTO = lift.CreateDTO(new ConvertConfig());
            Assert.IsNotNull(liftDTO);
            Assert.AreEqual(lift.ID, liftDTO.ID);
            Assert.AreEqual(lift.Name, liftDTO.Name);
            Assert.AreEqual(lift.ParentID, liftDTO.ParentID);

            Assert.IsNotNull(liftDTO.Details.Parent);
            Assert.AreEqual(lift.Parent.ID, liftDTO.Details.Parent.ID);
            Assert.AreEqual(lift.Parent.Name, liftDTO.Details.Parent.Name);

            Assert.IsNotNull(liftDTO.Details.Parent.Details.Lifts);
            Assert.AreEqual(liftDTO.Details.Parent.Details.Lifts.Count, 1);
            var childLiftDTO = liftDTO.Details.Parent.Details.Lifts[0];

            Assert.AreEqual(lift.ID, childLiftDTO.ID);
            Assert.AreEqual(lift.Name, childLiftDTO.Name);
            Assert.AreEqual(lift.ParentID, childLiftDTO.ParentID);
        }
    }
}
