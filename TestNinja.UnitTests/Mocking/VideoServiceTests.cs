using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            var service = new VideoService(new FakeFileReader()); // Dependencyc injection via Constructure

            //var result = service.ReadVideoTitle(new FakeFileReader()); // Dependencyc injection via Method Parameter
            var result = service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}