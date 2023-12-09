using Leopotam.EcsLite;


namespace HellishHive2
{
    internal class ServicesSystems
    {
        public ServicesSystems(EcsSystems systems,IPoolService poolService)
        {
            systems
                .Add(new InitializeServiceSystem(poolService));
        }
    }
}