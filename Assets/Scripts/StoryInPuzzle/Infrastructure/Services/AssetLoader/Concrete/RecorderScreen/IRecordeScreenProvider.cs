using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.RecorderScreen
{
    public interface IRecordeScreenProvider : IService
    {
        Task<RecordeScreen> Load();
        Task Unload();
    }
}