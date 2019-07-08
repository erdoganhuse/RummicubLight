using Game.Controller;
using Game.Logic.HandValueEstimationStrategy;
using Helper.PopupSystem.Scripts;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BoardController _boardController;
        [SerializeField] private UiController _uiController;
        [SerializeField] private PopupManager _gamePopupManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_boardController).AsSingle().NonLazy();
            Container.BindInstance(_uiController).AsSingle().NonLazy();
            Container.BindInstance(_gamePopupManager).AsSingle().NonLazy();
            Container.Bind<IHandValueEstimator>().To<DefaultHandValueEstimator>().AsSingle().NonLazy();
        }
    }
}