using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FloorMarker : MonoBehaviour
{
    [Inject] private readonly Constants _constants;
    [Inject] private readonly PlayerData _playerData;

    public int FloorNumber { get; private set; }

    public void Initialize(Model model)
    {
        FloorNumber = model.floorNumber;
        transform.position = model.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_constants.ELEVATOR_MARKER_TAG))
        {
            _playerData.SetFloor(FloorNumber);
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
