
using System;

[Serializable]
public class ClientData
{
    public readonly int arrivesOnFloor;
    public readonly int departsOnFloor;
    public readonly float arrivalTime;
    public readonly int id;

    public ClientData(int arrivesOnFloor, int departsOnFloor, float arrivalTime, int id)
    {
        this.arrivesOnFloor = arrivesOnFloor;
        this.departsOnFloor = departsOnFloor;
        this.arrivalTime = arrivalTime;
        this.id = id;
    }
}