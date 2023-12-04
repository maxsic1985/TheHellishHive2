using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    public class LeadBoardUILoadSystem : IEcsInitSystem
    {
        private EcsPool<IsLeadBoardMenu> _isMenu;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isMenu = world.GetPool<IsLeadBoardMenu>();
            _isMenu.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.LEADBOARD_MENU;
        }
    }
}