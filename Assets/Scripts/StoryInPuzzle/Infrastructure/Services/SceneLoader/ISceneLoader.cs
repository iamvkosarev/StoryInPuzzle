using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace StoryInPuzzle.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        Task<Scene> LoadScene(string name);
    }
}