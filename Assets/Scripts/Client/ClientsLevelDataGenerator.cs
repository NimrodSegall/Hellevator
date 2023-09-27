using Assets.Scripts.Math;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Client
{
    public class ClientsLevelDataGenerator
    {
        [Inject] private readonly Constants _constants;
        private List<ClientData> _clientsLevelData = new List<ClientData>();
        public ReadOnlyCollection<ClientData> ClientsData => _clientsLevelData.AsReadOnly();
        private int NumberOfClients => _clientsLevelData.Count;

        private float _minTimeBetweenSpawns = 10;
        private float _maxTimeBetweenSpawns = 30;

        private int numberOfFloors = 6;

        public ClientsLevelDataGenerator()
        {

        }

        public void GenerateNewLevelData()
        {
            var totalLevelDuration = _constants.LEVEL_DURATION_SECONDS;
            var currentTime = 0f;
            while (totalLevelDuration - currentTime > 0)
            {
                var arrivesOnFloor = GetRandomFloor();
                var departsOnFloor = GetRandomFloor();
                var arrivalTime = currentTime;
                var id = NumberOfClients;
                AddClient(new ClientData(arrivesOnFloor, departsOnFloor, arrivalTime, id));
                currentTime += Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
            }
        }

        private void AddClient(ClientData newData)
        {
            _clientsLevelData.Add(newData);
        }

        private int GetRandomFloor()
        {
            return Random.Range(0, numberOfFloors - 1);
        }


    }
}