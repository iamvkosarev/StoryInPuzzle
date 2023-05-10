namespace StoryInPuzzle.Infrastructure.Services.Config
{
    public class GameConfigProvider : IGameConfigProvider
    {
        public GameConfig GameConfig { get; }
        public GameConfigProvider(GameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }
    }
}