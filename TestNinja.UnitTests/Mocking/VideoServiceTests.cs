using NUnit.Framework;
using TestNinja.Mocking;
using Moq;
using System.Collections.Generic;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _service;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepo> _videoRepo;
        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>(); // Mocking Framework
            _videoRepo = new Mock<IVideoRepo>();
            _service = new VideoService(_fileReader.Object, _videoRepo.Object); // Pass Object via Mocking framework
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

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVidoesAreProcessed_ReturnAnEmptyString()
        {
            _videoRepo.Setup(repo => repo.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(string.IsNullOrEmpty(result));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_ReturnAStringWithIds()
        {
            _videoRepo.Setup(repo => repo.GetUnprocessedVideos()).Returns(new List<Video> { 
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result == "1,2,3");
        }
    }
}