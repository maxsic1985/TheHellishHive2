namespace HellishHive2
{
    public interface IAssetLoader
    {
        void LoadAsset(object address, Leopotam.EcsLite.EcsWorld ecsWorld, int entity);
    }
}
