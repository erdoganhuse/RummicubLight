using Constants;
using Game.Signal;
using UnityEngine;
using Zenject;

namespace Game.Controller
{
    public class GameController : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly SceneController _sceneController;
    
        public GameController(
            SignalBus signalBus,
            SceneController sceneController)
        {
            _signalBus = signalBus;
            _sceneController = sceneController;
        }
    
        ~GameController() { }
        
        public void Initialize()
        {
            StartGame();
        }

        private void StartGame()
        {
            _sceneController.LoadSceneAsync(SceneNames.Game);
            _signalBus.TryFire(new GameStartedSignal());
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;            
        }
    }
}
