using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen
{
    public interface ILoginScreenProvider : IService
    {
        Task<LoginScreen> Load();
        Task Unload();
    }
}