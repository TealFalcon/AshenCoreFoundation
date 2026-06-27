using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using VContainer;

namespace AshenCore.Core
{
    public interface ILog
    {
        void Log(string message);
        void Log(string message, ConsoleMessageType type);
        void Warn(string message);
        void Error(string message);
        void SetGUI(GUIConsoleController controller);
        void InitializeGUI();
        bool UseGUI();
        void ClearGUI();
        void SetVariable(string varName, string varValue);
        void SetRegisteredServices(List<string> registeredServices);
        List<string> GetRegisteredServices();
        bool GetDebugMode();
        void SetDebugCamera(GameObject camera);
    }

    public interface IAudioService
    {
        void PlayClip(AudioChannel channel, AudioClip clip);
        void PlayClip(AudioChannel channel, AudioClip clip, Vector3 position);
        void PlayClip(AudioChannel channel, AudioClip clip, Vector3 position, float volume);

    }

    public interface IACFeedbackService
    {
        IACScreenFeedbackService Screen { get; }
    }

    public interface IACScreenFeedbackService
    {
        IACInOut Fade { get; }
    }

    public interface IACInOut
    {
        void Initialize();
        void SetController(GameObject controller);
        UniTask FadeIn();
        UniTask FadeOut();
    }

    public interface ISpawnerService
    {
        ACSpawnHandle Spawn(ACSpawnRequest spawnRequest);
        bool Despawn(int id);
        bool Despawn(GameObject gameObject);
        void SetResolver(IObjectResolver resolver);
        UniTask PrewarmPool(ACPoolDefinition poolDefinition);
        ACSpawnHandle SpawnUI(GameObject prefab, Transform parent, bool activateOnSpawnl);
        UniTask CheckPools();
        ACSpawnHandle Spawn(int poolId, Vector3 position, Quaternion rotation, Transform parent, bool activateOnSpawn);
        ACSpawnHandle Spawn(int poolId, Vector3 position, Quaternion rotation, Transform parent);
        ACSpawnHandle Spawn(int poolId, Vector3 position, Quaternion rotation);
        ACSpawnHandle Spawn(int poolId, Vector3 position);
        ACSpawnHandle Spawn(int poolId);
        ACSpawnHandle Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent, bool activateOnSpawn, ACPool pool);
        ACSpawnHandle Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent);
        ACSpawnHandle Spawn(GameObject prefab, Vector3 position, Quaternion rotation);
        ACSpawnHandle Spawn(GameObject prefab, Vector3 position);
        ACSpawnHandle Spawn(GameObject prefab);
        UniTask ClearPools();
        ACPool GetPool(int id);
        UniTask ReInitializePool(ACPoolDefinition poolDefinition);
        ACSpawnHandle TakeFromPool(GameObject prefab);
        ACPool GetPoolByType(GameObject prefab);
        ACSpawnHandle TakeFromPool(int id);
        ACSpawnHandle TakeFromPool(ACPoolDefinition poolDefinition);
        ACSpawnHandle TakeFromPool(ACPool pool);
        bool ReturnToPool(ACSpawnHandle handle);
    }

    public interface IACInputSystem
    {
        event Action<ACInputEvent> OnInput;
        void SetCurrentContext(ACContextDefinition context);
        ACContextDefinition GetCurrentContext();
        void SetCurrentProfile(ACInputProfile profile);
        ACInputProfile GetCurrentProfile();
    }

    public interface IResourcesService
    {
        UniTask PreloadAsync(AssetHandle handle);
        bool IsPreloadable(AssetHandle handle);
        UniTask<T> LoadAsync<T>(AssetHandle handle) where T : UnityEngine.Object;
        void UnloadAsset(AssetHandle handle);
        UniTask<long> GetSizeAsync(AssetHandle handle);
    }


    public interface IACPersistenceSystem
    {

        public void Initialize();
        public ACSaveSystemType GetSaveSystemType();

        ACSaveService GetSaveService();

        ACSlot GetSlot(int slotId);
        int GetNextFreeSlotId(List<ACSaveMetaData> metas);
        public ACSlot GetCurrentSlot();

        //GLOBAL DATA
        List<ACSaveMetaData> GetAllSaves();
        List<ACSlot> GetAllSlots();
        bool HasSave(int slot);

        UniTask<ACSlot> CreateNewSave();
        UniTask<SaveResult> SaveAsync(int slot);
        UniTask<LoadResult> LoadAsync(int slot);
        UniTask<bool> DeleteAsync(int slotId);

        //SCENE DATA
        UniTask<LoadResult> LoadSceneData(int sceneId);
        UniTask<SaveResult> SaveSceneData(int sceneId);
        UniTask<bool> DeleteSceneData(int sceneId);

    }
    
        public interface IUIService
    {
        GUIHelper OpenWindow<T>();
        GUIHelper CloseWindow<T>();
        GUIHelper RemoveWindow<T>();
        List<string> GetRegisteredUIs();
        void Initialize();
        ACIconDefinition GetIcon(string iconId);
    }
    
}