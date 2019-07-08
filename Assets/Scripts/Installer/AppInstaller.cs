using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private TileVisualContainer _tileVisualContainer;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_tileVisualContainer).AsSingle().NonLazy();
        }
    }
}