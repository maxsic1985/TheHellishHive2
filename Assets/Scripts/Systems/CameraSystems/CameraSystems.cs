using Leopotam.EcsLite;

namespace HellishHive2
{
    public sealed class CameraSystems
    {
        public CameraSystems(EcsSystems systems)
        {
            systems
                .Add(new CameraSystem())
                .Add(new CameraInitSystem())
                .Add(new CameraBuildSystem());

        }
    }
}