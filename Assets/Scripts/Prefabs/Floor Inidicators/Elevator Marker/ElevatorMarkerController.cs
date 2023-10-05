using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

public class ElevatorMarkerController : 
    AnimatedController<ElevatorMarkerController, ElevatorMarkerView, ElevatorMarkerModel>
{
    [Inject(Id = MainGameWindowInstallerIds.ElevatorWindowScrollingBackground)]
    private readonly ScrollingBackgroundController _backgroundController;

    [Inject] private readonly FloorBarMarkersLayoutManager _floorBarMarkersLayoutManager;
    [Inject] private readonly PlayerData _playerDataController;

    public ReactiveProperty<int> ReachedTargetFloor;

    public override void Initialize(ElevatorMarkerModel model)
    {
        base.Initialize(model);
        _backgroundController.Initialize(new ScrollingBackgroundModel(5f, _view.animCurve));
    }

    public async UniTask GoToFloorNumber(int finalTargetFloorNumber)
    {
        var targetPosition = _floorBarMarkersLayoutManager.GetFloorPosition(finalTargetFloorNumber);
        var moveDuration = GetDurationToMoveBetweenFloors(
            _playerDataController.CurrentFloorNumber.Value, finalTargetFloorNumber);

        var scrollDistance = GetScrollingDistance(targetPosition);
        var backgroundScrollingTask = _backgroundController.ScrollVertically(scrollDistance, moveDuration);

        var markerMoveTask = _view.MoveElevatorMarkerToPosition(targetPosition, moveDuration);
        await UniTask.WhenAll(markerMoveTask, backgroundScrollingTask);
        ReachedTargetFloor.Value = finalTargetFloorNumber;
    }

    private float GetDurationToMoveBetweenFloors(int startFloor, int endFloor)
    {
        return Mathf.Abs(startFloor - endFloor) * _model.durationToMoveOneFloor;
    }

    private float GetScrollingDistance(Vector3 targetPosition)
    {
        var relativeVector = targetPosition - transform.position;
        var scrollDirection = -Mathf.Sign(relativeVector.y);
        return relativeVector.magnitude * scrollDirection;
    }

    public bool IsPlayingAnimation()
    {
        return DOTween.IsTweening(transform, true);
    }
}
