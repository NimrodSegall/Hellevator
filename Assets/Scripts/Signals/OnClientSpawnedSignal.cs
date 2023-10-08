
public class OnClientSpawnedSignal : ISignal
{
    public readonly int clientSpawnFloor;

    public OnClientSpawnedSignal(int clientSpawnFloor)
    {
        this.clientSpawnFloor = clientSpawnFloor;
    }
}
