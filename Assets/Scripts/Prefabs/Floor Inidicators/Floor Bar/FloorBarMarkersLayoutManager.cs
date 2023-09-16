using System;
using System.Collections.ObjectModel;
using UnityEngine;
using Zenject;

public class FloorBarMarkersLayoutManager : MonoBehaviour
{
    [Inject] private readonly FloorMarker.Pool _floorMarkerPool;

    [SerializeField] private Transform _topFloorMarkerLocation;
    [SerializeField] private Transform _bottomFloorMarkerLocation;

    private Vector3[] _floorPositions = new Vector3[0];
    public ReadOnlyCollection<Vector3> FloorPositions => Array.AsReadOnly(_floorPositions);

    public int NumberOfFloors => _floorPositions.Length;

    public void Initialize(int numberOfFloors)
    {
        GenerateFloorMarkers(numberOfFloors);
    }

    private void GenerateFloorMarkers(int numberOfFloors)
    {
        FillFloorMarkerPositionsFromTopToBottom(numberOfFloors);
        int numberOfFloor = 0;
        foreach (var floorPosition in _floorPositions)
        {
            var marker = _floorMarkerPool.Spawn(new FloorMarker.Model(numberOfFloor, floorPosition));
            numberOfFloor++;
        }
    }

    private Vector3[] FillFloorMarkerPositionsFromTopToBottom(int numberOfFloors)
    {
        _floorPositions = new Vector3[numberOfFloors];
        Vector3 topFloorPosition = _topFloorMarkerLocation.position;
        float yDistanceBetweenFloors = GetYDistBetweenFloors();

        for (int i = 0; i < numberOfFloors; i++)
        {
            _floorPositions[i] = topFloorPosition - i * new Vector3(0, yDistanceBetweenFloors, 0);
        }
        return _floorPositions;
    }

    public float GetYDistBetweenFloors()
    {
        return (_topFloorMarkerLocation.position.y - _bottomFloorMarkerLocation.position.y)
            / (NumberOfFloors - 1);
    }

    public int GetClosestFloorNumber(float yValue)
    {
        var dist = Mathf.Infinity;
        int nearestFloor = 0;
        for (int i = 0; i < _floorPositions.Length - 1; i++)
        {
            var currentDist = Mathf.Abs(_floorPositions[i].y - yValue);
            if (currentDist <= dist)
            {
                nearestFloor = i;
                dist = currentDist;
            }
        }
        return nearestFloor;
    }

    public Vector3 GetFloorPosition(int floorNumber)
    {
        return _floorPositions[floorNumber];
    }
}
