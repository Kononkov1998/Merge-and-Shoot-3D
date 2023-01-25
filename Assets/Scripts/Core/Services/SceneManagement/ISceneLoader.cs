using System;

namespace Core.Services.SceneManagement
{
    public interface ISceneLoader : IService
    {
        void ReloadCurrentScene(Action onLoaded = null);
        void LoadScene(string name, Action onLoaded = null);
        bool SceneLoaded(string name);
    }
}