using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.HelpGameScreen
{
    public interface IHelpGameScreenProvider : IService
    {
        Task<HelpGameScreen> Load();
        Task Unload();
    }
}