using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : View<PopupController, PopupView, PopupModel>
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _body;

    public override void Initialize(PopupModel model, PopupController controller)
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