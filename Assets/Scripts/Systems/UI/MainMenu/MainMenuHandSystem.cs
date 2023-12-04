using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace HalfDiggers.Runner
{
    public class MainMenuHandSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterShowSettings;
        private EcsFilter _filterQuit;
        private EcsFilter _filterRestart;
        private EcsFilter _filterLeadBoard;

        private EcsWorld _world;

        private EcsPool<BtnShowSettingMenu> _settingMenuPool;
        private EcsPool<BtnQuit> _quitPool;
        private EcsPool<BtnRestart> _menuRestartpool;
        private EcsPool<BtnShowLeadBoardMenu> _leadBoardBtnCommandPool;
        private EcsPool<IsRestartComponent> _isRestartPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterShowSettings = _world.Filter<BtnShowSettingMenu>().End();
            _filterLeadBoard = _world.Filter<BtnShowLeadBoardMenu>().End();
            _filterQuit = _world.Filter<BtnQuit>().End();
            _filterRestart = _world.Filter<BtnRestart>().End();

            _settingMenuPool = _world.GetPool<BtnShowSettingMenu>();
            _leadBoardBtnCommandPool = _world.GetPool<BtnShowLeadBoardMenu>();
            _quitPool = _world.GetPool<BtnQuit>();
            _menuRestartpool = _world.GetPool<BtnRestart>();
            _isRestartPool = _world.GetPool<IsRestartComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            ShowSettingMenu();
            ShowLeadBoardMenu();
            Quit();
            Restart();
        }

        private void Restart()
        {
            foreach (var entity in _filterRestart)
            {
                _isRestartPool.Add(entity);

                var menuPool = _world.GetPool<IsMainMenu>();
                ref var menu = ref menuPool.Get(entity);
                if (_quitPool.Has(entity))
                {
                    menu.MenuValue.SetActive(false);
                }

                var timeServise = Service<ITimeService>.Get();
                timeServise.Resume();
                _quitPool.Del(entity);
                _menuRestartpool.Del(entity);
            }
        }

        private void Quit()
        {
            foreach (var entity in _filterQuit)
            {
                //    var menuPool = _world.GetPool<IsMainMenu>();
                //    ref var menu = ref menuPool.Get(entity);

                if (_quitPool.Has(entity))
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }

                _quitPool.Del(entity);
            }
        }

        private void ShowSettingMenu()
        {
            foreach (var entity in _filterShowSettings)
            {
                var timeServise = Service<ITimeService>.Get();
                timeServise.Pause();

                var menuPool = _world.GetPool<IsSettingMenu>();
                ref var menu = ref menuPool.Get(entity);

                if (_settingMenuPool.Has(entity))
                {
                    menu.MenuValue.SetActive(true);
                }

                _settingMenuPool.Del(entity);
            }
        }


        private void ShowLeadBoardMenu()
        {
            foreach (var entity in _filterLeadBoard)
            {
                var timeServise = Service<ITimeService>.Get();
                timeServise.Pause();

                var menuPool = _world.GetPool<IsLeadBoardMenu>();
                ref var menu = ref menuPool.Get(entity);

                if (_leadBoardBtnCommandPool.Has(entity))
                {
                    menu.MenuValue.SetActive(true);
                }

                _leadBoardBtnCommandPool.Del(entity);
            }
        }
    }
}