using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ClientView : View<ClientController, ClientView, ClientModel>
{
    [SerializeField] private AnimationCurve _animCurve;

    private Transform _rootTransform;

    public override void Initialize(ClientModel model, ClientController controller)
    {
        base.Initialize(model, controller);
        _rootTransform = _controller.transform;
        _rootTransform.position = _model.standingPointOutsideElevator.position;
    }

    public async UniTask MoveTowards(Vector3 target, float duration)
    {
        await _rootTransform.DOMove(target, duration)
            .SetEase(_animCurve);
    }

    public async UniTask FlipXScale(float xScaleValue)
    {
        await _rootTransform.DOScaleX(xScaleValue, _model.flipAnimationDuration);
    }
}
