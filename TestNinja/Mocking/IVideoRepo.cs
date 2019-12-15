using System.Collections.Generic;

namespace TestNinja.Mocking
{
    public interface IVideoRepo
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}