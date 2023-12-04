﻿using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    public class LeadBoardUIInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<IsLeadBoardMenu> _leadBoardObjectPool;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsLeadBoardMenu>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is UILeadBoardMenuData dataInit)
                {
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Menu;
                    
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}