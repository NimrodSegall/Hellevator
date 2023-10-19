using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientView : View<ClientController, ClientView, ClientModel>
{
    [SerializeField] private GameObject _spriteObject;
    [SerializeField] private TMP_Text _targetFloorText;
    [SerializeField] private Slider _patienceSlider;
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
        await _spriteObject.transform.DOScaleX(xScaleValue, _model.flipAnimationDuration);
    }

    public void SetTargetFloorText()
    {
        _targetFloorText.text = _model.departsOnFloorNum.ToString();
    }

    public void AddPatience(float amount)
    {
        _patienceSlider.value += amount;
    }

    public void SubtractPatience(float amount)
    {
        AddPatience(-amount);
    }
}
