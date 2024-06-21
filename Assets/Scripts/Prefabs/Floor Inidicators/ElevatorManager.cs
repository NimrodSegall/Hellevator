using Assets.Scripts.Player;
using Assts.Scripts.Signals;
using Cysharp.Threading.Tasks;
using Zenject;

public class ElevatorManager
{
    [Inject] private readonly PlayerData _playerData;

    [Inject] private readonly CameraShakeController _cameraShakeController;
    [Inject] private readonly ElevatorMarkerController _elevatorMarkerController;
    [Inject] private readonly FloorBarController _floorBarController;
    [Inject] private readonly FloorMeterController _floorMeterController;
    [Inject] private readonly ElevatorDoorsController _doorsController;
    [Inject] private readonly SignalBus _signalBus;

    public void Initialize()
    {
        _cameraShakeController.InitializeWithExistingModel();
        _elevatorMarkerController.Initialize(new ElevatorMarkerModel(1.5f));
        _floorBarController.Initialize(new FloorBarModel());
        _floorMeterController.Initialize(new FloorMeterModel());
        _doorsController.Initialize(new ElevatorDoorsModel(1.25f, 1.25f));

        // _barMarkerController.CurrentFloorNumber.Subscribe(x => _floorMeterController.SetFloor(x));
    }

    public async UniTask MoveElevatorToFloor(int floorNumber)
    {
        await _doorsController.CloseDoors();
        await _elevatorMarkerController.GoToFloorNumber(floorNumber);
        await _cameraShakeController.ShakeElevator();
        await _doorsController.OpenDoors();

        var currentFloorNumber = _playerData.CurrentFloorNumber.Value;
        _signalBus.Fire(new OnElevatorReachedDestinationSignal(currentFloorNumber));
    }
}
