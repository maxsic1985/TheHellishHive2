using Leopotam.EcsLite;
using UnityEngine;


namespace HellishHive2
{
    public class DEBUG_TestPostProcessingSystems : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<PostProcessingComponent> _isPostProcessingPool;
        private EcsFilter _filter;
        private PlayerSharedData _sharedData;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _filter = world.Filter<PostProcessingComponent>().End();
            _isPostProcessingPool = world.GetPool<PostProcessingComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var postProcessing = ref _isPostProcessingPool.Get(entity);
                if (postProcessing.VignetteValue == null) return;

                if (Input.GetKeyDown(KeyCode.I))
                {
                    _sharedData.GetPlayerCharacteristic.UpdateCoins(1);
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    _sharedData.GetPlayerCharacteristic.UpdateCoins(2);

                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        _sharedData.GetPlayerCharacteristic.UpdateCoins(3);
                    }

                    switch (_sharedData.GetPlayerCharacteristic.GetCurrentCoins)
                    {
                        case 3:
                            postProcessing.IntensivityValue = postProcessing.InitialIntensivityValue;
                            break;
                        case 2:
                            postProcessing.IntensivityValue = postProcessing.TwoLifeIntensivityValue;
                            break;
                        case 1:
                            postProcessing.IntensivityValue = postProcessing.OneLifeIntensivityValue;
                            break;
                        default:
                            postProcessing.IntensivityValue = postProcessing.InitialIntensivityValue;
                            break;
                    }
                }
            }
        }
    }
}