using Assets.Scripts.Signals;
using Zenject;

namespace Assts.Scripts.Signals
{
    public class MainGameSceneSignalssInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<OnElevatorReachedDestinationSignal>();
            Container.DeclareSignal<OnElevatorButtonClickedSignal>();
            Container.DeclareSignal<OnClientSpawnedSignal>();
            Container.DeclareSignal<OnClientsFinishedMovingAroundElevatorSignal>();
            Container.DeclareSignal<OnClientPatienceRanOut>();
        }
    }
}