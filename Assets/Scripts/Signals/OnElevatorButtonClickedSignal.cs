using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnElevatorButtonClickedSignal : ISignal
{
    public readonly int floorNumber;

    public OnElevatorButtonClickedSignal(int floorNumber)
    {
        this.floorNumber = floorNumber;
    }
}
