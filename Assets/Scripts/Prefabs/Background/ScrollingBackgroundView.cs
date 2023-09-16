using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ScrollingBackgroundView : View
    <ScrollingBackgroundController, ScrollingBackgroundView, ScrollingBackgroundModel>
{

    public async UniTask ScrollVertically(float scrollDistance, float scrollDuration)
    {
        var endPosition = transform.position + 
            new Vector3(0, _model.scrollingDistanceMultiplier * scrollDistance, 0);
        await transform.DOMove(endPosition, scrollDuration).SetEase(_model.animationCurve);
    }
}
