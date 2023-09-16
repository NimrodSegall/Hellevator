using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClientsManager
{
    [Inject(Id = MainGameWindowInstallerIds.ClientsStandingSpotsInElevator)]
    private readonly Transform[] _clientsStandingSpotsInElevator;

    [Inject(Id = MainGameWindowInstallerIds.ClientsStandingSpotOutsideElevator)]
    private readonly Transform _clientsStandingSpotOutsideElevator;

    [Inject] private readonly ClientController.Pool _clientControllerPool;
    [Inject] private readonly SignalBus _signalBus;
    [Inject] private readonly PlayerData _playerData;
    [Inject] private readonly ClientsDataHandler _clientsDataHandler;

    public Transform[] ClientsStandingSpotsInElevator => _clientsStandingSpotsInElevator;

    private List<ClientController> _clients = new List<ClientController>();
    private List<ClientModel> _clientModelsToSpawn = new List<ClientModel>();

    public void Initialize()
    {
        _clientModelsToSpawn.Add(new ClientModel(
            _clientsStandingSpotsInElevator[0],
            _clientsStandingSpotOutsideElevator,
            2f,
            2f,
            3,
            1
            ));
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

    public ClientModel CreateNewClientModel(int originFloor, int targetFloor)
    {
        var clientModel = new ClientModel(
            _clientsStandingSpotsInElevator[0],
            _clientsStandingSpotOutsideElevator,
            3f,
            1f,
            originFloor,
            targetFloor);
        return clientModel;
    }

    private List<ClientController> GetNewClients(int floor)
    {
        var newClients = new List<ClientController>();
        foreach (var clientModel in _clientModelsToSpawn)
        {
            if (clientModel.originFloor == floor)
            {
                newClients.Add(_clientControllerPool.Spawn(clientModel));
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
