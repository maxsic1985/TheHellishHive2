using Leopotam.EcsLite;


namespace HellishHive2
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