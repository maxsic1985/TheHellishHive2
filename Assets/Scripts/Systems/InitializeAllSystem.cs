using Leopotam.EcsLite;

namespace HellishHive2
{
    internal class InitializeAllSystem
    {
        public InitializeAllSystem(
            EcsSystems systems,
            IPoolService poolService,
            bool isMainMenu)
        {

            new ServicesSystems(systems, poolService);
            new LoadResoursesSystems(systems);
            new CommonSystems(systems);
            new GameRuntimeSystems(systems);
        }
    }


    public class GameRuntimeSystems
    {
        public GameRuntimeSystems(EcsSystems systems)
        {
            new CameraSystems(systems);
        }
    }
}