namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen
{
    public class LevelTaskScreenProvider : AssetProvider<LevelTaskScreen>, ILevelTaskScreenProvider
    {
        private int _taskScreenIndex;

        public void SetTaskScreenIndex(int index)
        {
            _taskScreenIndex = index;
        }

        protected override string AssetKey => AssetsKeys.LevelTaskScreen(_taskScreenIndex);
        
    }
}