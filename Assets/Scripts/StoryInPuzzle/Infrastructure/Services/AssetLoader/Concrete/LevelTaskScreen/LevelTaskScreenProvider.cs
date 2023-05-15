namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen
{
    public class LevelTaskScreenProvider : AssetProvider<LevelTaskScreen>, ILevelTaskScreenProvider
    {

        protected override string AssetKey => AssetsKeys.LevelTaskScreen;
        
    }
}