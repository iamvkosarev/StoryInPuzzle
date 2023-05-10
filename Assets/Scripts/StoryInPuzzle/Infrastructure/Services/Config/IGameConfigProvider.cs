namespace StoryInPuzzle.Infrastructure.Services.Config
{
    public interface IGameConfigProvider : IService
    {
        GameConfig GameConfig { get; }
    }
}