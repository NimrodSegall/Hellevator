using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using Assets.Scripts.Utils;

public class ClientController : AnimatedController<ClientController, ClientView, ClientModel>
{
    public int TargetFloor => _model.departsOnFloorNum;

    [Inject] private readonly DeltaTimer _patienceTimer;

    public override void Initialize(ClientModel model)
    {
        base.Initialize(model);
        SetTargetFloorText();
        _patienceTimer.Initialize(model.patienceDuration, OnPatienceRunsOut, OnPatienceTickDown);
    }

    public async UniTask MoveToElevator()
    {
        await MoveTowards(_model.standingPointInElevator.position, _model.moveAnimationDuration);
        await _view.FlipXScale(-transform.localScale.x);
    }

    public async UniTask MoveOutsideElevator()
    {
        await MoveTowards(_model.standingPointOutsideElevator.position, _model.moveAnimationDuration);
    }

    private async UniTask MoveTowards(Vector3 target, float duration)
    {
        if (_isBusy)
        {
            return;
        }

        SetBusy();
        await _view.MoveTowards(target, duration);
        SetNotBusy();
    }

    private void SetTargetFloorText()
    {
        _view.SetTargetFloorText();
    }

    private void OnPatienceRunsOut()
    {
        Debug.Log("Patience ran out!");
    }

    private void OnPatienceTickDown()
    {
        _view.SubtractPatience(Time.deltaTime / _model.patienceDuration);
    }

    public class Factory : PlaceholderFactory<ClientModel, ClientController>
    {
        public override ClientController Create(ClientModel param)
        {
            var client = base.Create(param);
            client.Initialize(param);
            return client;
        }
    }

    public class Pool : MonoMemoryPool<ClientModel, ClientController>
    {
        protected override void Reinitialize(ClientModel p1, ClientController item)
        {
            base.Reinitialize(p1, item);
            item.Initialize(p1);
            item.transform.localScale = new Vector3(Mathf.Abs(item.transform.localScale.x),
                item.transform.localScale.y,
                item.transform.localScale.z);
        }
    }
}
