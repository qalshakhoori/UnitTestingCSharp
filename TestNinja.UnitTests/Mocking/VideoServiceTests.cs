using NUnit.Framework;
using TestNinja.Mocking;
using Moq;
namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _service;
        private Mock<IFileReader> _fileReader;
        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>(); // Mocking Framework
            _service = new VideoService(_fileReader.Object); // Pass Object via Mocking framework
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            //var service = new VideoService(new FakeFileReader()); // Dependencyc injection via Constructure

            //var result = service.ReadVideoTitle(new FakeFileReader()); // Dependencyc injection via Method Parameter

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}