
using Assets.Scripts.Player;
using Zenject;
using UniRx;

public class FloorMeterController : MonoController<FloorMeterController, FloorMeterView, FloorMeterModel>
{
    [Inject] private readonly PlayerData _playerData;

    private int _currentFloorNumber;
    public int CurrentFloorNumber => _currentFloorNumber;

    public override void Initialize(FloorMeterModel model)
    {
        base.Initialize(model);
        _playerData.CurrentFloorNumber.Subscribe(SetFloor);
    }

    public void SetFloor(int newFloorNumber)
    {
        _currentFloorNumber = newFloorNumber;
        _view.SetFloor(newFloorNumber);
    }
}
