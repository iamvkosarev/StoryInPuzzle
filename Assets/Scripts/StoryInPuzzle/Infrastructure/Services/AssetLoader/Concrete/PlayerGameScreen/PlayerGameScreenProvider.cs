namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen
{
    public class PlayerGameScreenProvider : AssetProvider<PlayerGameScreen>, IPlayerGameScreenProvider
    {
        protected override string AssetKey => AssetsKeys.PlayerGameScreen;
    }
}