using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class ElevatorButton : MonoStreamListener<OnElevatorReachedDestinationSignal>
{
    [Inject] private readonly ElevatorButtonsManager _elevatorButtonsManager;

    [SerializeField] private int _floorNumber;
    [SerializeField] private GameObject _neutralButtonObject;
    [SerializeField] private TextMeshProUGUI _neutralButtonText;
    [SerializeField] private GameObject _selectedButtonObject;
    [SerializeField] private TextMeshProUGUI _selectedButtonText;

    private Button _neutralButton;
    private Button _selectedButton;

    private bool _isSelected;
    public bool IsSelected => _isSelected;

    private void Awake()
    {
        _neutralButton = _neutralButtonObject.GetComponent<Button>();
        _selectedButton = _selectedButtonObject.GetComponent<Button>();
        if (_neutralButton == null || _selectedButton == null)
        {
            Debug.LogError("One or more buttons not found");
        }

        Initialize();

        SetButtonText(_floorNumber.ToString());
        AddOnClickListener(OnElevatorButtonClicked);
        SetButtonSelectedState(false);
    }

    private void SetButtonSelectedState(bool isSelected)
    {
        _isSelected = isSelected;
        _selectedButtonObject.SetActive(isSelected);
        _neutralButtonObject.SetActive(!isSelected);
        _elevatorButtonsManager.SetButtonSelectedState(this);
    }

    private void SetButtonText(string text)
    {
        _neutralButtonText.text = _floorNumber.ToString();
        _selectedButtonText.text = _floorNumber.ToString();
    }

    private void AddOnClickListener(UnityAction action)
    {
        _neutralButton.onClick.AddListener(action);
        _selectedButton.onClick.AddListener(action);
    }

    private void OnElevatorButtonClicked()
    {
        if (!_elevatorButtonsManager.IsAnyButtonSelected())
        {
            _signalBus.Fire(new OnElevatorButtonClickedSignal(_floorNumber));
            SetButtonSelectedState(true);
        }
    }

    public override void InvokedFromSignal(OnElevatorReachedDestinationSignal signal)
    {
        base.InvokedFromSignal(signal);
        if (signal.floorNumberReached == _floorNumber)
        {
            SetButtonSelectedState(false);
        }
    }
}
