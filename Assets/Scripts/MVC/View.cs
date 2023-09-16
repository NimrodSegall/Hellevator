using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View<TController, TView, TModel> : MonoBehaviour 
    where TController : MonoController<TController, TView, TModel>
    where TView : View<TController, TView, TModel>
{
    protected TController _controller;
    protected TModel _model;

    public virtual void Initialize(TModel model, TController controller)
    {
        _model = model;
        _controller = controller;
    }
}
