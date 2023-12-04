using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    internal class TriggerSystems
    {
        public TriggerSystems(EcsSystems systems)
        {
            systems.Add(new TriggerSystem());
        }
    }
}