using Assets.Scripts.Popup;
using UnityEngine;
using Zenject;

public class MainGameWindowPrefabsInstaller : MonoInstaller
{
    [SerializeField] private ClientController _clientPrefab;
    [SerializeField] private Transform _clientsPoolTransform;
    [SerializeField] private FloorMarker _floorMarkerPrefab;
    [SerializeField] private Transform _floorMarkerPoolTransform;
    [SerializeField] private GameOverPopupController _gameOverPopupControllerPrefab;
    [SerializeField] private Transform _popupsTransform;

    private const int CLIENT_POOL_INITIAL_SIZE = 15;
    private const int MARKER_POOL_INITIAL_SIZE = 10;

    public override void InstallBindings()
    {
        Container.BindFactory<ClientModel, ClientController, ClientController.Factory>()
          .FromComponentInNewPrefab(_clientPrefab);

        Container.BindMemoryPool<ClientController, ClientController.Pool>()
            .WithInitialSize(CLIENT_POOL_INITIAL_SIZE)
            .FromComponentInNewPrefab(_clientPrefab)
            .UnderTransform(_clientsPoolTransform);

        Container.BindFactory<FloorMarker.Model, FloorMarker, FloorMarker.Factory>()
            .FromComponentInNewPrefab(_floorMarkerPrefab);

        Container.BindMemoryPool<FloorMarker, FloorMarker.Pool>()
            .WithInitialSize(MARKER_POOL_INITIAL_SIZE)
            .FromComponentInNewPrefab(_floorMarkerPrefab)
            .UnderTransform(_floorMarkerPoolTransform);

        Container.Bind<GameOverPopupController>()
            .FromComponentInNewPrefab(_gameOverPopupControllerPrefab)
            .UnderTransform(_popupsTransform)
            .AsSingle();
    }
}