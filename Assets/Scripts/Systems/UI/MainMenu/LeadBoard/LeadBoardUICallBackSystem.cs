using HalfDiggers.Runner;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.Scripting;

namespace HalfDiggers
{
    sealed class LeadBoardUICallBackSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<BtnHideMenu> _hideBtnCommandPool;
       

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsLeadBoardMenu>().End();
            _hideBtnCommandPool = _world.GetPool<BtnHideMenu>();
        }
        
     
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.LEADBOARD_CLOSE, WorldsNamesConstants.EVENTS)]
        void OnClickQuit(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                _hideBtnCommandPool.Add(entity);
            }
        }
    }
}   