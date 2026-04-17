using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Develop.Runtime.Gameplay.Infrastructure
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
            
            yield return new WaitWhile(() => operation.isDone == false);
        }

        public IEnumerator UnloadAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
            
            yield return new WaitWhile(() => operation.isDone == false);
        }
    }
}

