using Assets.Scripts.Player;
using Assets.Scripts.Utils;
using Zenject;

public class ProjectContextMainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var playerDataController = new PlayerData();

        Container.Bind<PlayerData>()
            .FromInstance(playerDataController);
        Container.QueueForInject(playerDataController);

        var constants = new Constants();
        Container.Bind<Constants>()
            .FromInstance(constants);

        Container.Bind<Timer>()
            .AsTransient();
        Container.Bind<ITickable>()
            .To<Timer>()
            .AsTransient();
    }
}