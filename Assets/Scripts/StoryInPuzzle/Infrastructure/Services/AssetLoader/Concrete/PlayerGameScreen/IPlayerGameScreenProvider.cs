using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen
{
    public interface IPlayerGameScreenProvider : IService
    {
        Task<PlayerGameScreen> Load();
        Task Unload();
    }
}