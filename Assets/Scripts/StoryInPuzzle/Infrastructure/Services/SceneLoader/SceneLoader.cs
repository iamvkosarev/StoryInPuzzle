using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace StoryInPuzzle.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async Task<Scene> LoadScene(string sceneName)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            if (sceneName == SceneManager.GetActiveScene().name)
            {
                Debug.LogWarning($"Scene '{sceneName}' is already loaded.");
                return SceneManager.GetActiveScene();
            }

            var handle = Addressables.LoadSceneAsync(sceneName);
            await handle.Task;
            stopwatch.Stop();
            //Debug.Log("Загрузка сцены - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
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