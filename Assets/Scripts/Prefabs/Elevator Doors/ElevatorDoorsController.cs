using Cysharp.Threading.Tasks;

public class ElevatorDoorsController : AnimatedController<ElevatorDoorsController, ElevatorDoorsView, ElevatorDoorsModel>
{
    public async UniTask OpenDoors()
    {
        await _view.OpenDoors();
    }

    public async UniTask CloseDoors()
    {
        await _view.CloseDoors();
    }
}
