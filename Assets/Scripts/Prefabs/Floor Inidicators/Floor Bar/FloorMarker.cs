using Assets.Scripts.Player;
using Assts.Scripts.Signals;
using TMPro;
using UnityEngine;
using Zenject;

public class FloorMarker : MonoStreamListener
    <OnElevatorReachedDestinationSignal,
    OnClientSpawnedSignal>
{
    [Inject] private readonly Constants _constants;
    [Inject] private readonly PlayerData _playerData;
    [SerializeField] private GameObject _highlightIndicatorObject;
    [SerializeField] private TMP_Text _floorNumberText;

    public int FloorNumber { get; private set; }

    public void Initialize(Model model)
    {
        base.Initialize();
        FloorNumber = model.floorNumber;
        _floorNumberText.text = model.floorNumber.ToString();
        transform.position = model.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_constants.ELEVATOR_MARKER_TAG))
        {
            _playerData.SetFloor(FloorNumber);
        }
    }

    private void HighlightSetActive(bool isActive)
    {
        _highlightIndicatorObject.SetActive(isActive);
    }

    public override void InvokedFromSignal(OnClientSpawnedSignal signal)
    {
        if (signal.clientSpawnFloor == FloorNumber)
        {
            base.InvokedFromSignal(signal);
            HighlightSetActive(true);
        }
    }

    public override void InvokedFromSignal(OnElevatorReachedDestinationSignal signal)
    {
        if (signal.floorNumberReached == FloorNumber)
        {
            base.InvokedFromSignal(signal);
            HighlightSetActive(false);
        }
    }

    public class Factory : PlaceholderFactory<Model, FloorMarker>
    {
        public override FloorMarker Create(Model model)
        {
            var client = base.Create(model);
            client.Initialize(model);
            return client;
        }
    }

    public class Pool : MonoMemoryPool<Model, FloorMarker>
    {
        protected override void Reinitialize(Model p1, FloorMarker item)
        {
            base.Reinitialize(p1, item);
            item.Initialize(p1);
            item.transform.localScale = new Vector3(Mathf.Abs(item.transform.localScale.x),
                item.transform.localScale.y,
                item.transform.localScale.z);
        }
    }

    public class Model
    {
        public readonly int floorNumber;
        public readonly Vector3 position;

        public Model(int floorNumber, Vector3 position)
        {
            this.floorNumber = floorNumber;
            this.position = position;
        }
    }
}
