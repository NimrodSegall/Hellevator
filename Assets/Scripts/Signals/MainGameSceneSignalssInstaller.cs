using Zenject;

public class MainGameSceneSignalssInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<OnElevatorReachedDestinationSignal>();
        Container.DeclareSignal<OnElevatorButtonClickedSignal>();
        Container.DeclareSignal<OnClientsFinishedMovingAroundElevatorSignal>();
    }
}