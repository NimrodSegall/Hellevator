using Assets.Scripts.Client;
using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MainGameSceneManager : MonoStreamListener<OnElevatorButtonClickedSignal>
{
    [Inject] private readonly PlayerData _playerData;

    [Inject] private readonly ClientsManager _clientsManager;
    [Inject] private readonly ElevatorManager _elevatorManager;
    [Inject] private readonly FloorBarMarkersLayoutManager _elevatorBarMarkersManager;

    private const int TOTAL_NUMBER_OF_FLOORS = 5;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        _elevatorBarMarkersManager.Initialize(TOTAL_NUMBER_OF_FLOORS);
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
