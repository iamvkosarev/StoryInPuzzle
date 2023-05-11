namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.RecorderScreen
{
    public class RecordeScreenProvider : AssetProvider<RecordeScreen>, IRecordeScreenProvider
    {
        protected override string AssetKey => AssetsKeys.RecordeScreen;
    }
}