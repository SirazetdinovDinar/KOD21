using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Services
{
    public class ScenesService
    {
        public static async Task LoadScene(string sceneName, LoadSceneMode mode, bool forceReload = false)
        {
            if (!forceReload && IsLoaded(sceneName))
            {
                return;
            }

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, new LoadSceneParameters(mode));
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }

        public static async Task UnloadScene(string sceneName)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            while (asyncOperation != null && !asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }

        public static bool IsLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.isLoaded && scene.name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}