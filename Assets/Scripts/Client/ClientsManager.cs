using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Client
{
    public class ClientsManager : ITickable
    {
        [Inject(Id = MainGameWindowInstallerIds.ClientsStandingSpotsInElevator)]
        private readonly Transform[] _clientsStandingSpotsInElevator;

        [Inject(Id = MainGameWindowInstallerIds.ClientsStandingSpotOutsideElevator)]
        private readonly Transform _clientsStandingSpotOutsideElevator;

        [Inject] private readonly ClientController.Pool _clientControllerPool;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly PlayerData _playerData;
        [Inject] private readonly ClientsLevelDataGenerator _clientsDataHandler;
        [Inject] private readonly NewActiveClientsProvider _newActiveClientsProvider;

        public Transform[] ClientsStandingSpotsInElevator => _clientsStandingSpotsInElevator;

        private List<ClientController> _clients = new List<ClientController>();
        private List<ClientModel> _clientModelsToSpawn = new List<ClientModel>();

        public void Initialize()
        {
            _clientsDataHandler.GenerateNewLevelData();
            _newActiveClientsProvider.Initialize(_clientsDataHandler.ClientsData, Time.time);
        }

        public void Tick()
        {
            var newClientsData = _newActiveClientsProvider.GetNewActiveClients(Time.time);
            foreach (var data in newClientsData)
            {
                var clientModel = new ClientModel(
                    _clientsStandingSpotsInElevator[0],
                    _clientsStandingSpotOutsideElevator,
                    3f,
                    1f,
                    data.arrivesOnFloor,
                    data.departsOnFloor,
                    data.id,
                    data.clientPatianceTime);
                _clientModelsToSpawn.Add(clientModel);
                Debug.Log($"New client: From {data.arrivesOnFloor} to {data.departsOnFloor}");
                _signalBus.Fire(new OnClientSpawnedSignal(data.arrivesOnFloor));
            }    
        }

        public async UniTask OnElevatorReachedDestination()
        {
            var currentFloor = _playerData.CurrentFloorNumber.Value;
            var newClients = GetNewClients(currentFloor);
            var leavingClients = GetAllCurrentClientsWithDestination(currentFloor);
            var moveNewClientsToElevatorTask = MoveAllNewClientsToElevator(newClients);
            var moveLeavingClientsOutsideElevatorTask = MoveLeavingClientsOutsideElevator(leavingClients);
            await UniTask.WhenAll(moveNewClientsToElevatorTask, moveLeavingClientsOutsideElevatorTask);

            _clients.RemoveAll(client => leavingClients.Contains(client));
            _clients.AddRange(newClients);
            _signalBus.Fire(new OnClientsFinishedMovingAroundElevatorSignal());
        }

        private List<ClientController> GetNewClients(int floor)
        {
            var newClients = new List<ClientController>();
            foreach (var clientModel in _clientModelsToSpawn.ToArray())
            {
                if (clientModel.arrivesOnFloorNum == floor)
                {
                    newClients.Add(_clientControllerPool.Spawn(clientModel));
                    _clientModelsToSpawn.Remove(clientModel);
                }
            }
            return newClients;
        }

        private List<ClientController> GetAllCurrentClientsWithDestination(int destinationFloor)
        {
            var currentClients = new List<ClientController>();
            foreach (var client in _clients)
            {
                if (client.TargetFloor == destinationFloor)
                {
                    currentClients.Add(client);
                }
            }
            return currentClients;
        }

        private async UniTask MoveAllNewClientsToElevator(List<ClientController> newClients)
        {
            var tasks = new List<UniTask>();
            foreach (var client in newClients)
            {
                tasks.Add(client.MoveToElevator());
            }
            await UniTask.WhenAll(tasks);
        }

        private async UniTask MoveLeavingClientsOutsideElevator(List<ClientController> currentClients)
        {
            var tasks = new List<UniTask>();
            foreach (var client in currentClients)
            {
                tasks.Add(client.MoveOutsideElevator());
            }
            await UniTask.WhenAll(tasks);
        }
    }
}