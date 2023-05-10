using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen
{
    public interface ISelectLevelScreenProvider : IService
    {
        Task<SelectLevelScreen> Load();
        Task Unload();
    }
}