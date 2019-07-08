using Game.UI.Screens;
using Helper.PopupSystem.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Controller
{
    public class UiController : MonoBehaviour
    {
        private PopupManager _gamePopupManager;
        
        [Inject]
        private void Construct(PopupManager gamePopupManager)
        {
            _gamePopupManager = gamePopupManager;
        }

        private void Start()
        {
            Initialize();
        }
        
        public void Initialize()
        {
            _gamePopupManager.Open<MainScreen>();
        }
    }
}
