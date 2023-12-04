using Leopotam.EcsLite;

namespace HellishHive2
{
    public class PostProcessingSystems
    {
        
        public PostProcessingSystems(EcsSystems systems)
        {
            systems
                .Add(new DEBUG_TestPostProcessingSystems())
                .Add(new PostProcessingSystem())
                .Add(new PostProcessingVigneteSystem())
                .Add(new PostProcessingInitSystem())
                .Add(new PostprocessingBuildSystem());
        }
    }
}