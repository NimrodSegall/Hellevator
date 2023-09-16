

using UnityEngine;

public class ScrollingBackgroundModel
{
    public readonly float scrollingDistanceMultiplier;
    public readonly AnimationCurve animationCurve;

    public ScrollingBackgroundModel(float scrollingDistanceMultiplier, AnimationCurve animationCurve)
    {
        this.scrollingDistanceMultiplier = scrollingDistanceMultiplier;
        this.animationCurve = animationCurve;
    }
}
