using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    public class SceneLoader
    {
        private ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public Task Load(string name, Action onLoaded = null) => LoadScene(name, onLoaded);

        private async Task LoadScene(string name, Action onLoaded = null)
        {
            if (!string.Equals(SceneManager.GetActiveScene().name, name))
            {
                var waitNextScene = SceneManager.LoadSceneAsync(name);
                while (!waitNextScene.isDone)
                    await Task.Yield();
            }
            onLoaded?.Invoke();
        }
    }
}