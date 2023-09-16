using UnityEngine;

public class ClientModel
{
    public readonly Transform standingPointInElevator;
    public readonly Transform standingPointOutsideElevator;
    public readonly float moveAnimationDuration;
    public readonly float flipAnimationDuration;
    public readonly int originFloor;
    public readonly int targetFloor;

    public ClientModel(
        Transform standingPointInElevator,
        Transform standingPointOutsideElevator,
        float moveAnimationDuration,
        float flipAnimationDuration,
        int originFloor,
        int targetFloor
        )
    {
        this.standingPointInElevator = standingPointInElevator;
        this.standingPointOutsideElevator = standingPointOutsideElevator;
        this.moveAnimationDuration = moveAnimationDuration;
        this.flipAnimationDuration = flipAnimationDuration;
        this.originFloor = originFloor;
        this.targetFloor = targetFloor;
    }
}
