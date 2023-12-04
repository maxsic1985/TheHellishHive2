using Leopotam.EcsLite;



namespace HellishHive2
{
    internal class CommonSystems
    {
        public CommonSystems(EcsSystems systems)
        {
            systems.Add(new TransformMovingSystem());
            systems.Add(new SynchronizeTransformAndPositionSystem());
            systems.Add(new TimerRunSystem());
            systems.Add(new OnGroundSystem());
            systems.Add(new GravitySystem());
        }
    }
}