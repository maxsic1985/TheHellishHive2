using Leopotam.EcsLite;
using LeopotamGroup.Globals;


namespace HalfDiggers.Runner
{
    public class LeadBoardUIHandSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterHieMenu;

        private EcsPool<BtnHideMenu> _menuHidePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterHieMenu = _world.Filter<BtnHideMenu>().Inc<IsLeadBoardMenu>().End();
            _menuHidePool = _world.GetPool<BtnHideMenu>();
        }


        public void Run(IEcsSystems systems)
        {
            HideMenu();
        }


        private void HideMenu()
        {
            foreach (var entity in _filterHieMenu)
            {
                var menuPool = _world.GetPool<IsLeadBoardMenu>();
                ref var menu = ref menuPool.Get(entity);

                if (_menuHidePool.Has(entity))
                {
                    menu.MenuValue.SetActive(false);
                }

                var timeServise = Service<ITimeService>.Get();
                timeServise.Resume();
                _menuHidePool.Del(entity);
            }
        }
    }
}