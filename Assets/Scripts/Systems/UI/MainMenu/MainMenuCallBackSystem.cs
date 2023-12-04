using HalfDiggers.Runner;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.Scripting;

namespace HalfDiggers
{
    sealed class MainMenuCallBackSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsFilter _filtersetting;
        private EcsFilter _filterLeadBoard;
        private EcsWorld _world;
        
        private EcsPool<BtnQuit> _quitBtnCommandPool;
        private EcsPool<BtnRestart> _restartBtnCommandPool;
        private EcsPool<BtnShowSettingMenu> _settingBtnCommandPool;
        private EcsPool<BtnShowLeadBoardMenu> _leadBoardBtnCommandPool;
        private EcsPool<IsRestartComponent> _isRestartPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsMainMenu>().End();
            _filtersetting = _world.Filter<IsSettingMenu>().End();
            _filterLeadBoard = _world.Filter<IsLeadBoardMenu>().End();
            _quitBtnCommandPool = _world.GetPool<BtnQuit>();
            _restartBtnCommandPool = _world.GetPool<BtnRestart>();
            _settingBtnCommandPool = _world.GetPool<BtnShowSettingMenu>();
            _leadBoardBtnCommandPool = _world.GetPool<BtnShowLeadBoardMenu>();
        }
        

        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.MENU_RESTART, WorldsNamesConstants.EVENTS)]
        void OnClickRestart(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
              //  _isRestartPool.Add(entity);
                _restartBtnCommandPool.Add(entity);
            }
        }
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.MENU_SETTING, WorldsNamesConstants.EVENTS)]
        void OnClickSetting(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filtersetting)
            {
                _settingBtnCommandPool.Add(entity);
            }
        }
        
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.LEADBOARD_SHOW, WorldsNamesConstants.EVENTS)]
        void OnClickForward(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filterLeadBoard)
            {
                _leadBoardBtnCommandPool.Add(entity);
            }
        }
     
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.MENU_QUIT, WorldsNamesConstants.EVENTS)]
        void OnClickQuit(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filtersetting)
            {
                _quitBtnCommandPool.Add(entity);
            }
        }
    }
}   