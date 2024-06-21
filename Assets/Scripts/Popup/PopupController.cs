using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Popup
{
    public abstract class PopupController<TPopupController, TPopupView, TPopupModel>
    : MonoController<TPopupController, TPopupView, TPopupModel>
        where TPopupController : PopupController<TPopupController, TPopupView, TPopupModel>
        where TPopupView : PopupView<TPopupController, TPopupView, TPopupModel>
        where TPopupModel : PopupModel
    {
        public override void Initialize(TPopupModel model)
        {
            base.Initialize(model);
            _view.SetInitialTexts();
        }

        public virtual void Close()
        {
            Destroy(this);
        }
    }
}
