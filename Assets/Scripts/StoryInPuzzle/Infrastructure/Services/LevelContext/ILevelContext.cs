using Core.Common;

namespace StoryInPuzzle.Infrastructure.Services.LevelContext
{
    public interface ILevelContext : IService
    {
        PlayerComponent PlayerComponent { set; get; }
        int LevelIndex { get; set; }
    }
}