using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.UnitTest.TheRack.Repository
{
    [TestClass]
    public class UserRepository_UnitTest
    {
        #region Private Field(s).
        private Mock<DomainContext> context;
        #endregion

        #region Test Initialize.
        [TestInitialize]
        public void Init()
        {
            var data = new List<User>
            {
                new User { ID = 1, Name = "Test", UserName = "test", Password = "123" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            context = new Mock<DomainContext>();
            context.Setup(c => c.Users).Returns(mockSet.Object);
        }
        #endregion

        #region Test Cleanup.
        [TestCleanup]
        public void Cleanup()
        {
            context.Object.Dispose();
        } 
        #endregion

        [TestMethod]
        public void LoginValid_Test()
        {
            using(var repository = new UserRepository(context.Object))
            {
                var r = repository.Login("test", "123");
                Assert.IsNotNull(r);
                Assert.AreEqual(r.ID, 1);
                Assert.AreEqual(r.Name, "Test");
                Assert.AreEqual(r.UserName, "test");
                Assert.AreEqual(r.Password, "123");
            }
        }
    }
}
