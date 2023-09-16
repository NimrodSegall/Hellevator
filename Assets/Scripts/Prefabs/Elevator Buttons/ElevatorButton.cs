using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ElevatorButton : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private int _floorNumber;
    private TextMeshProUGUI _buttonText;
    private Button _button; 

    private void Awake()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (_buttonText != null)
        {
            _buttonText.text = _floorNumber.ToString();
        }

        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => _signalBus.Fire(new OnElevatorButtonClickedSignal(_floorNumber)));
    }
}
