using UnityEngine;

public class MonoController<TController, TView, TModel> : MonoBehaviour 
    where TView : View<TController, TView, TModel>
    where TController : MonoController<TController, TView, TModel>
{
    [SerializeField] protected TView _view;
    protected TModel _model;

    public virtual void Initialize(TModel model)
    {
        _model = model;
        _view.Initialize(_model, (TController)this);
    }
}
