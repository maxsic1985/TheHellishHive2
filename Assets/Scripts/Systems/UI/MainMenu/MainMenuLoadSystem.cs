using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    public class MainMenuLoadSystem : IEcsInitSystem
    {
        private EcsPool<IsMainMenu> _isMenu;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isMenu = world.GetPool<IsMainMenu>();
            _isMenu.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.MAIN_MENU;
        }
    }
}