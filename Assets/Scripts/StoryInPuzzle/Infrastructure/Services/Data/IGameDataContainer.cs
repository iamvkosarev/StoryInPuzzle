namespace StoryInPuzzle.Infrastructure.Services.Data
{
    public interface IGameDataContainer : IService
    {
        GameData GameData { get; set; }
    }
}