using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            var repo = new Mock<IEmployeeRepo>();
            var controller = new EmployeeController(repo.Object);

            controller.DeleteEmployee(1);

            repo.Verify(r => r.DeleteEmployee(1));
        }

        [Test] 
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            var repo = new Mock<IEmployeeRepo>();
            var controller = new EmployeeController(repo.Object);

            var result =  controller.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
