using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private Action _onLoadedAction;

        public void ReloadCurrentScene(Action onLoaded = null) => 
            LoadScene(SceneManager.GetActiveScene().name, onLoaded);

        public void LoadScene(string name, Action onLoaded = null)
        {
            _onLoadedAction = onLoaded;
            AsyncOperation loading = SceneManager.LoadSceneAsync(name);
            loading.completed += OnLoadCompleted;
        }

        public bool SceneLoaded(string name) => 
            string.Equals(SceneManager.GetActiveScene().name, name);

        private void OnLoadCompleted(AsyncOperation loading) => 
            _onLoadedAction?.Invoke();
    }
}