using UnityEngine;

public class AnimatedController<TController, TView, TModel> : MonoController<TController, TView, TModel>
    where TView : View<TController, TView, TModel>
    where TController : AnimatedController<TController, TView, TModel>
{
    public bool IsBusy => _isBusy;
    protected bool _isBusy = false;
    protected int _numberOfBusySources = 0;

    public virtual void SetBusy()
    {
        _numberOfBusySources++;
        if (_numberOfBusySources > 0)
        {
            _isBusy = true;
        }
        else
        {
            Debug.LogError("SetBusy function was called but number of busy sources is" +
                $" {_numberOfBusySources} (must be at least 1)");
        }
    }

    public virtual void SetNotBusy()
    {
        _numberOfBusySources--;
        if (_numberOfBusySources == 0)
        {
            _isBusy = false;
        }
        else if (_numberOfBusySources < 0)
        {
            Debug.LogError($"number of busy sources is negative ({_numberOfBusySources}), " +
                $"are you calling {nameof(SetNotBusy)} without calling {nameof(SetBusy)} ?");
        }
    }
}
