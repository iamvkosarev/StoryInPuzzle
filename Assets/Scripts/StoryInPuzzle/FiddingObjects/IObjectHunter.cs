using StoryInPuzzle.Infrastructure.Services.LevelProgress;

namespace StoryInPuzzle.FiddingObjects
{
    public interface IObjectHunter
    {
        void SetLevelProgress(ILevelProgress levelProgress);
    }
}