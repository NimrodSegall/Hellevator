using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ElevatorMarkerView :
    View<ElevatorMarkerController, ElevatorMarkerView, ElevatorMarkerModel>
{
    [SerializeField] public AnimationCurve animCurve;
    public async UniTask MoveElevatorMarkerToPosition(Vector3 newPosition, float moveDuration)
    {
        await _controller.transform.DOMove(newPosition, moveDuration)
            .SetEase(animCurve);
    }
}
