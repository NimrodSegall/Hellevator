using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoController<PopupController, PopupView, PopupModel>
{
    public override void Initialize(PopupModel model)
    {
        base.Initialize(model);
        _view.SetInitialTexts();
    }

    public virtual void Close()
    {
        Destroy(this);
    }
}
