namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.HelpGameScreen
{
    public class HelpGameScreenProvider : AssetProvider<HelpGameScreen>, IHelpGameScreenProvider
    {
        protected override string AssetKey => AssetsKeys.HelpGameScreen;
    }
}