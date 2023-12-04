using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    internal class InitializeAllSystem
    {
        public InitializeAllSystem(EcsSystems systems, IPoolService poolService, IPatternService patternService,
            bool isMainMenu)
        {

            new ServicesSystems(systems, poolService, patternService);
            new LoadResoursesSystems(systems);
            new CommonSystems(systems);

            if (isMainMenu)  new MainMenuSystems(systems);
            if (isMainMenu) return;
            new GameRuntimeSystems(systems);
        }
    }


    public class GameRuntimeSystems
    {
        public GameRuntimeSystems(EcsSystems systems)
        {
            new TreadMillSystems(systems);
            new PlayerSystems(systems);
            new CameraSystems(systems);
            new PauseMenuSystems(systems);
            new PostProcessingSystems(systems);
            new SoundSystems(systems);
            new DataManagerSystems(systems);
            new DeathSystems(systems);
            new TriggerSystems(systems);
        }
    }
}