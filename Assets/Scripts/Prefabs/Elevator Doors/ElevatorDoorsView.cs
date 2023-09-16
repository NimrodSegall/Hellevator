using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorsView : View<ElevatorDoorsController, ElevatorDoorsView, ElevatorDoorsModel>
{
    [SerializeField] private Transform _doorL;
    [SerializeField] private Transform _doorR;

    [SerializeField] private Transform _doorOpenLocationTransformL;
    [SerializeField] private Transform _doorOpenLocationTransformR;

    [SerializeField] private AnimationCurve _animCurve;

    private DoorPosition _doorPositionL;
    private DoorPosition _doorPositionR;

    public override void Initialize(ElevatorDoorsModel model, ElevatorDoorsController controller)
    {
        base.Initialize(model, controller);
        _doorPositionL = new DoorPosition(_doorOpenLocationTransformL.position, _doorL.position);
        _doorPositionR = new DoorPosition(_doorOpenLocationTransformR.position, _doorR.position);
    }

    public async UniTask OpenDoors()
    {
        var leftDoorAnimationTask = _doorL.DOMove(_doorPositionL.open, _model.doorOpenAnimationDuration)
            .SetEase(_animCurve)
            .ToUniTask();
        var rightDoorAnimationTask = _doorR.DOMove(_doorPositionR.open, _model.doorOpenAnimationDuration)
            .SetEase(_animCurve)
            .ToUniTask();
        await UniTask.WhenAll(leftDoorAnimationTask, rightDoorAnimationTask);
    }

    public async UniTask CloseDoors()
    {
        var leftDoorAnimationTask = _doorL.DOMove(_doorPositionL.close, _model.doorCloseAnimationDuration)
            .SetEase(_animCurve)
            .ToUniTask();
        var rightDoorAnimationTask = _doorR.DOMove(_doorPositionR.close, _model.doorCloseAnimationDuration)
            .SetEase(_animCurve)
            .ToUniTask();
        await UniTask.WhenAll(leftDoorAnimationTask, rightDoorAnimationTask);
    }

    private class DoorPosition
    {
        public readonly Vector3 open;
        public readonly Vector3 close;

        public DoorPosition(Vector3 positionOpen, Vector3 positionClose)
        {
            open = positionOpen;
            close = positionClose;
        }
    }
}
