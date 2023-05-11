using Core.Common;

namespace StoryInPuzzle.Infrastructure.Services.LevelContext
{
    public class LevelContext : ILevelContext
    {
        public PlayerComponent PlayerComponent { set; get; }
        public int LevelIndex { get; set; }
    }
}