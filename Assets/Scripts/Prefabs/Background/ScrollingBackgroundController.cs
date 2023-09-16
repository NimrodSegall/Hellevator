using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundController : MonoController
    <ScrollingBackgroundController, ScrollingBackgroundView, ScrollingBackgroundModel>
{
    public async UniTask ScrollVertically(float scrollDistance, float scrollDuration)
    {
        await _view.ScrollVertically(scrollDistance, scrollDuration);
    }
}
