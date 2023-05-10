namespace StoryInPuzzle.Infrastructure.Services.Data
{
    public class GameDataContainer : IGameDataContainer
    {
        public GameData GameData { get; set; }

        public GameDataContainer()
        {
            GameData = new GameData();
        }
    }
}