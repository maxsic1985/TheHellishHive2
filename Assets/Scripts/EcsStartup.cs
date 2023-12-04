using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;


namespace HellishHive2
{
    public sealed class EcsStartup : MonoBehaviour
    {
        public EcsSystems Systems { get; private set; }
        private bool _hasInitCompleted;
        [SerializeField] EcsUguiEmitter uguiEmitter;
        [SerializeField] bool isMainMenu=false;
        
        

        private async void Start()
        {
            Application.targetFrameRate = 60;
            SharedData shared = new();
            await shared.Init();

            IPoolService poolService = new PoolService();
            await poolService.Initialize();

            var world = new EcsWorld();
            Systems = new EcsSystems(world,shared);

            new InitializeAllSystem(Systems, poolService,isMainMenu);

            Systems
                .AddWorld (new EcsWorld (), WorldsNamesConstants.EVENTS)
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldsNamesConstants.EVENTS))

#endif
               
                .InjectUgui (uguiEmitter, WorldsNamesConstants.EVENTS)
                .Init();
            _hasInitCompleted = true;
        }

        private void Update()
        {
            if (_hasInitCompleted)
                Systems?.Run();
        }

        private void OnDestroy()
        {
            if (Systems != null)
            {
                foreach (var worlds in  Systems.GetAllNamedWorlds())
                {
                    worlds.Value.Destroy();
                }
                Systems.GetWorld().Destroy();
                Systems = null;
            }
        }
    }
}