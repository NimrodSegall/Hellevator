using UnityEngine;

public class ClientModel
{
    public readonly Transform standingPointInElevator;
    public readonly Transform standingPointOutsideElevator;
    public readonly float moveAnimationDuration;
    public readonly float flipAnimationDuration;
    public readonly int arrivesOnFloorNum;
    public readonly int departsOnFloorNum;
    public readonly int id;
    public readonly float patienceDuration;

    public ClientModel(
        Transform standingPointInElevator,
        Transform standingPointOutsideElevator,
        float moveAnimationDuration,
        float flipAnimationDuration,
        int arrivesOnFloorNum,
        int departsOnFloorNum,
        int id,
        float patienceDuration
        )
    {
        this.standingPointInElevator = standingPointInElevator;
        this.standingPointOutsideElevator = standingPointOutsideElevator;
        this.moveAnimationDuration = moveAnimationDuration;
        this.flipAnimationDuration = flipAnimationDuration;
        this.arrivesOnFloorNum = arrivesOnFloorNum;
        this.departsOnFloorNum = departsOnFloorNum;
        this.id = id;
        this.patienceDuration = patienceDuration;
    }
}
