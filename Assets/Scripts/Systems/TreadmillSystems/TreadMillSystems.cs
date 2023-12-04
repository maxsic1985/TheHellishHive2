using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    internal class TreadMillSystems
    {
        public TreadMillSystems(EcsSystems systems)
        {
            systems.Add(new LoadTreadmillDataSystem())
                .Add(new TreadmillInitSystem())
                .Add(new TreadmillBuildSystem())
                .Add(new MovePlatformSystem())
                .Add(new CheckPositionPlatformSystem())
                .Add(new RespawnPlatformSystem())
                .Add(new InstantiateLampSystem())
                .Add(new InstantiatePickableObjectsSystem())
                .Add(new AccelerationPlatformSystem());
        }
    }
}