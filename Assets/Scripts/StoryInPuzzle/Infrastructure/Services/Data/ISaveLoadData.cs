using System.Threading.Tasks;

namespace StoryInPuzzle.Infrastructure.Services.Data
{
    public interface ISaveLoadData : IService
    {
        Task Load();
        Task Save();
    }
}