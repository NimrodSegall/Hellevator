using UnityEngine;
using Zenject;
using Cinemachine;

public class MainGameWindowInstaller : MonoInstaller
{
    [SerializeField] private Transform[] _clientsStandingSpotsInElevator;
    [SerializeField] private Transform _clientsStandingSpotOutsideElevator;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private FloorBarController _floorBarController;
    [SerializeField] private FloorMeterController _floorMeterController;
    [SerializeField] private ElevatorDoorsController _doorsController;
    [SerializeField] private ScrollingBackgroundController _windowBackgroundController;
    [SerializeField] private FloorBarMarkersLayoutManager _floorMarkersLayoutManager;
    [SerializeField] private ElevatorMarkerController _elevatorMarkerController;

    public override void InstallBindings()
    {
        Container.Bind<Transform[]>()
            .WithId(MainGameWindowInstallerIds.ClientsStandingSpotsInElevator)
            .To<Transform[]>()
            .FromInstance(_clientsStandingSpotsInElevator)
            .AsSingle();

        Container.Bind<Transform>()
            .WithId(MainGameWindowInstallerIds.ClientsStandingSpotOutsideElevator)
            .To<Transform>()
            .FromInstance(_clientsStandingSpotOutsideElevator)
            .AsSingle();

        ClientsManager _clientsManager = new ClientsManager();
        Container.Bind<ClientsManager>()
            .FromInstance(_clientsManager)
            .AsSingle();
        Container.QueueForInject(_clientsManager);

        ElevatorManager _elevatorManager = new ElevatorManager();
        Container.Bind<ElevatorManager>()
            .FromInstance(_elevatorManager)
            .AsSingle();
        Container.QueueForInject(_elevatorManager);

        ClientsDataHandler _clientsDataHandler = new ClientsDataHandler();
        Container.Bind<ClientsDataHandler>()
            .FromInstance(_clientsDataHandler)
            .AsSingle();
        Container.QueueForInject(_clientsDataHandler);

        Container.Bind<FloorBarMarkersLayoutManager>()
        .FromInstance(_floorMarkersLayoutManager)
        .AsSingle();

        Container.Bind<CinemachineVirtualCamera>()
            .WithId(MainGameWindowInstallerIds.VirtualCamera)
            .FromInstance(_virtualCamera)
            .AsSingle();

        Container.Bind<CameraShakeController>()
            .FromMethod(CreateAndQueueForInjectCameraShakeController)
            .AsSingle();

        Container.Bind<FloorBarController>()
            .FromInstance(_floorBarController)
            .AsSingle();

        Container.Bind<ElevatorMarkerController>()
            .FromInstance(_elevatorMarkerController)
            .AsSingle();

        Container.Bind<FloorMeterController>()
            .FromInstance(_floorMeterController)
            .AsSingle();
        Container.QueueForInject(_floorMeterController);

        Container.Bind<ElevatorDoorsController>()
            .FromInstance(_doorsController)
            .AsSingle();

        Container.Bind<ScrollingBackgroundController>()
            .WithId(MainGameWindowInstallerIds.ElevatorWindowScrollingBackground)
            .FromInstance(_windowBackgroundController)
            .AsSingle();
    }

    private CameraShakeController CreateAndQueueForInjectCameraShakeController()
    {
        var model = new CameraShakeModel(
            250,
            0.25f,
            2000,
            500);
        CameraShakeController cameraShakeController = new CameraShakeController(model);
        Container.QueueForInject(cameraShakeController);
        return cameraShakeController;
    }
}