using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    internal class LoadResoursesSystems
    {
        public LoadResoursesSystems(EcsSystems systems)
        {
            systems.Add(new LoadPrefabSystem());
            systems.Add(new LoadDataByNameSystem());
        }
    }
}