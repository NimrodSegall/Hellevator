
using UnityEngine;

public class Controller<TModel>
{
    protected TModel _model;

    public Controller(TModel model)
    {
        Initialize(model);
    }

    public virtual void Initialize(TModel model)
    {
        _model = model;
    }

    public virtual void InitializeWithExistingModel()
    {
        if (_model == null)
        {
            Debug.LogError($"Attempted to initialize a {GetType()} " +
                $"without a model, did you want to use {nameof(Initialize)} instead?");
        }
    }
}
