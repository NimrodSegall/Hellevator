using UnityEngine;
using Zenject;

public class FloorBarInstaller : MonoInstaller
{
    [SerializeField] private FloorBarController _floorBarController;
    [SerializeField] private ElevatorMarkerController _elevatorMarkerController;
    public override void InstallBindings()
    {
        Container.Bind<FloorBarController>()
            .FromInstance(_floorBarController);

        Container.Bind<ElevatorMarkerController>()
            .FromInstance(_elevatorMarkerController);
    }
}
