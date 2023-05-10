using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace StoryInPuzzle.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async Task<Scene> LoadScene(string sceneName)
        {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                Debug.LogWarning($"Scene '{sceneName}' is already loaded.");
                return SceneManager.GetActiveScene();
            }

            var handle = Addressables.LoadSceneAsync(sceneName);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                SceneManager.SetActiveScene(handle.Result.Scene);
                return handle.Result.Scene;
            }

            Debug.LogError($"Failed to load scene '{sceneName}'.");
            return default;
        }
    }
}