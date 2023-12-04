using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    internal class ServicesSystems
    {
        public ServicesSystems(EcsSystems systems,IPoolService poolService, IPatternService patternService)
        {
            systems
                .Add(new InitializeServiceSystem(poolService, patternService));
        }
    }
}