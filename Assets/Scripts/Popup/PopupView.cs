using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Popup
{
    public abstract class PopupView<TPopupController, TPopupView, TPopupModel>
    : View<TPopupController, TPopupView, TPopupModel>
        where TPopupController : PopupController<TPopupController, TPopupView, TPopupModel>
        where TPopupView : PopupView<TPopupController, TPopupView, TPopupModel>
        where TPopupModel : PopupModel
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _body;

        public override void Initialize(TPopupModel model, TPopupController controller)
        {
            base.Initialize(model, controller);
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(ClosePopup);
            }
        }

        virtual protected void ClosePopup()
        {
            _controller.Close();
        }

        public void SetInitialTexts()
        {
            if (_title != null)
            {
                _title.text = _model.title;
            }

            if (_body != null)
            {
                _body.text = _model.body;
            }
        }
    }
}