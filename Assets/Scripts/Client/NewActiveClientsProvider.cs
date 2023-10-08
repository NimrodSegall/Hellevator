using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Client
{
    public class NewActiveClientsProvider
    {
        [Inject] private readonly SignalBus _signalBus;
        private float startTime;
        private List<ClientData> _clientsData;

        public void Initialize(IReadOnlyCollection<ClientData> clientsData, float currentTime)
        {
            startTime = currentTime;
            _clientsData = clientsData.ToList();
        }

        public List<ClientData> GetNewActiveClients(float currentTime)
        {
            var newActiveClients = new List<ClientData>();
            foreach (var clientData in _clientsData.ToArray())
            {
                if (clientData.arrivalTime <= DeltaTime(currentTime))
                {
                    newActiveClients.Add(clientData);
                    _clientsData.Remove(clientData);
                }
            }
            return newActiveClients;
        }

        private float DeltaTime(float currentTime)
        {
            return currentTime - startTime;
        }
    }
}