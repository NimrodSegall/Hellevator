using Assets.Scripts.Client;
using Assets.Scripts.Player;
using Assts.Scripts.Signals;
using UnityEngine;
using Zenject;

public class MainGameSceneManager : MonoStreamListener<OnElevatorButtonClickedSignal>
{
    [Inject] private readonly PlayerData _playerData;

    [Inject] private readonly ClientsManager _clientsManager;
    [Inject] private readonly ElevatorManager _elevatorManager;
    [Inject] private readonly FloorBarMarkersLayoutManager _elevatorBarMarkersManager;

    [SerializeField] private Transform _floorButtonsParent;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        var totalNumberOfFloors = _floorButtonsParent.childCount;
        _elevatorBarMarkersManager.Initialize(totalNumberOfFloors);
        _elevatorManager.Initialize();
        _clientsManager.Initialize();
    }

    public override async void InvokedFromSignal(OnElevatorButtonClickedSignal signal)
    {
        base.InvokedFromSignal(signal);
        var chosenFloorNumber = signal.floorNumber;
        await _elevatorManager.MoveElevatorToFloor(chosenFloorNumber);
        await _clientsManager.OnElevatorReachedDestination();

    }
}
