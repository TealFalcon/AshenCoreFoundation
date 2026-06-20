using UnityEngine;
using System.Collections.Generic;
using VContainer;
using Cysharp.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;



namespace AshenCore.Core
{

    public enum ACScenes
    {
        PersistentLayer = 1,
        DebugScene = 2,
        Basic2DScene = 100,
        Basic3DScene = 101
    }

    public enum ACGameScenes
    {
        //ADD GAME SCENES HERE
    }

    public enum ACMenuScenes
    {
        //ADD MENU SCENES HERE
    }


    public class ACSceneManager : MonoBehaviour, IGlobalPersistenceProvider
    {

        private ACSceneContext _sceneContext = new ACSceneContext();
        public List<ACSceneDefinition> sceneDefinitions;
        public string Key => "SCENEMANAGER";
        public ACSceneDefinition LastScene;
        public ACSceneDefinition CurrentScene;
        public IACSceneHandle CurrentSceneHandle;

        public SceneData currentSceneData = new SceneData();
        public ILog Log;

        [Inject]
        void Construct(AshenCoreServices services)
        {

            _sceneContext.audioService = services.Audio;
            _sceneContext.eventService = services.Events;
            _sceneContext.fadeService = services.Fade;
            _sceneContext.spawnerService = services.Spawner;
            _sceneContext.debugSystem = services.Debug;
            _sceneContext.SceneManager = this;
            _sceneContext.resourcesService = services.Resources;
            _sceneContext.persistenceService = services.Persistence;

            Log = services.Debug;

            services.Persistence?.GetSaveService()?.RegisterGlobalProvider(this);

        }


        public async UniTask LoadScene(int sceneId)
        {

            ACSceneDefinition sceneDefinition = null;
            
            Log?.Log("[ASHEN CORE] Loading Scene " + sceneId, ConsoleMessageType.Info);

            foreach (var sd in sceneDefinitions)
            {
                if (sd.SceneId == sceneId)
                {
                    sceneDefinition = sd;
                    break;
                }
            }

            if (sceneDefinition == null)
            {
                Log?.Log("[ASHEN CORE] Scene " + sceneId + " not found", ConsoleMessageType.Error);
                return;
            }

            Log?.Log("[ASHEN CORE] Loading " + sceneDefinition.Alias, ConsoleMessageType.Info);

            if (sceneDefinition.ScenePipeline == null
            || sceneDefinition.ScenePipeline._steps == null
            || sceneDefinition.ScenePipeline._steps.Length <= 0)
            {
                Log?.Log("[ASHEN CORE] Scene " + sceneDefinition.Alias + " has no pipeline", ConsoleMessageType.Error);
                return;
            }

            foreach (ACLoadStep step in sceneDefinition.ScenePipeline._steps)
            {

                Log?.Log("[ASHEN CORE] Executing " + step.GetType().Name, ConsoleMessageType.Info);
                await step.Execute(sceneDefinition, _sceneContext);
            }

            return;
        }


        public object Capture()
        {
            return currentSceneData;
        }

        public void Restore(object data)
        {
            if (data is JObject jObject)
            {
                currentSceneData = jObject.ToObject<SceneData>();
            }

            if (currentSceneData is SceneData sceneData)
            {
                Log?.Log("SCENE ID" + sceneData.sceneId, ConsoleMessageType.Info);
                Log?.Log("SPAWN POINT ID" + sceneData.spawnPointId, ConsoleMessageType.Info);

            }
            else
            {
                Log?.Log("DATA IS NULL", ConsoleMessageType.Error);
            }

        }



        async void Awake()
        {
            //Open PersistentLayer Scene 
            await LoadScene(1);
        }


        public List<ACSceneDefinition> GetSceneDefinitions()
        {
            return sceneDefinitions;
        }



    }

    public class ACSceneContext
    {
        public ACAudioService audioService;
        public ACEventService eventService;
        public ACFadeService fadeService;
        public ACSpawnerService spawnerService;
        public ILog debugSystem;
        public ACSceneManager SceneManager;
        public ACResourcesService resourcesService;
        public IACPersistenceSystem persistenceService;
    }

    public enum ACSceneType
    {
        Game = 0,
        Menu = 1,
        System = 2
    }

    [Serializable]
    public class SceneData
    {
        public int sceneId;
        public int spawnPointId;
    }

    public interface IACSceneHandle
    {
        UniTask Unload();
    }

    public class AddressableSceneHandle : IACSceneHandle
    {
        readonly AsyncOperationHandle<SceneInstance> _handle;

        public AddressableSceneHandle(AsyncOperationHandle<SceneInstance> handle)
        {
            _handle = handle;
        }

        public async UniTask Unload()
        {
            await Addressables.UnloadSceneAsync(_handle);
        }
    }

    public class BuiltInSceneHandle : IACSceneHandle
    {
        readonly Scene _scene;

        public BuiltInSceneHandle(Scene scene)
        {
            _scene = scene;
        }

        public async UniTask Unload()
        {
            await SceneManager.UnloadSceneAsync(_scene);
        }
    }



}