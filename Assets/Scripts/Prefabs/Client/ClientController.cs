using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class ClientController : AnimatedController<ClientController, ClientView, ClientModel>
{
    public int TargetFloor => _model.targetFloor;

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
