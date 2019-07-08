using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Controller
{
    public class SceneController
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly List<String> _loadedScenes;

        private Action _onSceneLoaded;
        private Action _onSceneUnloaded;
        
        public SceneController(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _loadedScenes = new List<string>();
        }
        
        public void LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
        {
            if (_loadedScenes.Contains(sceneName)) return;
            
            _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _onSceneLoaded = onSceneLoaded;
            
            SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
        }

        public void UnloadSceneAsync(string sceneName, Action onSceneUnloaded = null)
        {
            if (!_loadedScenes.Contains(sceneName)) return;

            SceneManager.UnloadSceneAsync(sceneName);
            _onSceneUnloaded = onSceneUnloaded;
            
            SceneManager.sceneUnloaded += SceneManager_OnSceneUnloaded; 
        }

        #region Event Listeners

        private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _loadedScenes.Add(scene.name);

            _onSceneLoaded?.Invoke();
            _onSceneLoaded = null;
        }
        
        private void SceneManager_OnSceneUnloaded(Scene scene)
        {
            _loadedScenes.Remove(scene.name);
            
            _onSceneUnloaded?.Invoke();
            _onSceneUnloaded = null;
        }

        #endregion
    }
}
