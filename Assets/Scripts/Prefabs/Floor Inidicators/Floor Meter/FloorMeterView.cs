using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorMeterView : View<FloorMeterController, FloorMeterView, FloorMeterModel>
{
    [SerializeField] TMP_Text _floorNumberText;

    public void SetFloor(int floorNumber)
    {
        _floorNumberText.text = floorNumber.ToString();
    }
}
