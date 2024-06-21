
namespace Assts.Scripts.Signals
{
    public class OnElevatorReachedDestinationSignal : ISignal
    {
        public readonly int floorNumberReached;

        public OnElevatorReachedDestinationSignal(int floorNumberReached)
        {
            this.floorNumberReached = floorNumberReached;
        }


    }
}
