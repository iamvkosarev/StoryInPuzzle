namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen
{
    public class SelectLevelScreenProvider : AssetProvider<SelectLevelScreen>, ISelectLevelScreenProvider
    {
        protected override string AssetKey => AssetsKeys.SelectLevelScreen;
    }
}