using Game.Controller;
using Game.Signal;
using Zenject;

namespace Installer
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneController>().AsSingle().NonLazy();
            
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStartedSignal>();
        }
    }
}