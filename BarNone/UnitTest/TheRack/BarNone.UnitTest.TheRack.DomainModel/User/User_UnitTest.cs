using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.UnitTest.TheRack.DomainModel
{
    [TestClass]
    public class User_UnitTest
    {
        [TestMethod]
        public void BuildDTO_Test()
        {
            var user = new User();
            var dto = user.CreateDTO(new ConvertConfig());

            Assert.IsNotNull(dto);
        }
    }
}
