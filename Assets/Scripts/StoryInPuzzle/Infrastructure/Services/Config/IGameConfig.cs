using StoryInPuzzle.Infrastructure.Services.Config.Concrete;

namespace StoryInPuzzle.Infrastructure.Services.Config
{
    public interface IGameConfig : IService
    {
        LevelsConfig LevelsConfig { get; }
    }
}