
namespace Assts.Scripts.Signals
{
    public class OnElevatorButtonClickedSignal : ISignal
    {
        public readonly int floorNumber;

        public OnElevatorButtonClickedSignal(int floorNumber)
        {
            this.floorNumber = floorNumber;
        }
    }
}
