using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class MainMenuSystems
    {
        public MainMenuSystems(EcsSystems systems)
        {
            MainMenuBuidSystem(systems);
            SettingSystems(systems);
            LeadBoardSystem(systems);
        }

        private static void MainMenuBuidSystem(EcsSystems systems)
        {
            systems
                .Add(new MainMenuLoadSystem())
                .Add(new MainMenuInitSystem())
                .Add(new MainMenuBuildSystem())
                .Add(new MainMenuCallBackSystem())
                .Add(new MainMenuHandSystem());
        }


        private static void SettingSystems(EcsSystems systems)
        {
            systems
                .Add(new SettingMenuLoadSystem())
                .Add(new SettingMenuInitSystem())
                .Add(new SettingMenuBuildSystem())
                .Add(new SettingMenuSystem())
                .Add(new SettingMenuCallBackSystem())
                .Add(new SettingSliderVolumeSystem());
        }


        private static void LeadBoardSystem(EcsSystems systems)
        {
            new LeadBoardUISystems(systems);
        }
    }
}