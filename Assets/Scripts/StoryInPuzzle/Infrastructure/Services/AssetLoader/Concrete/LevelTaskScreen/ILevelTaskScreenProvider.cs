using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen
{
    public interface ILevelTaskScreenProvider : IService
    {
        Task<LevelTaskScreen> Load();
        Task Unload();
    }
}