using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen
{
    public interface ILevelTaskScreenProvider : IService
    {
        void SetTaskScreenIndex(int index);
        Task<LevelTaskScreen> Load();
        Task Unload();
    }
}