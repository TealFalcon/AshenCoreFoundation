using UnityEngine;
using Cysharp.Threading.Tasks;
using VContainer;

namespace AshenCore.Core
{

    public enum SpawnedObjectState
    {
        Spawned,
        Active,
        Inactive,
        Despawned
    }

    public interface IPoolable
    {
        void SetId(int id);
        int GetId();
        void SetState(SpawnedObjectState state);
        SpawnedObjectState GetState();
        void OnSpawn();
        void OnDespawn();
        void OnTakenFromPool();
        void OnReturnedToPool();
        bool IsActive();
        bool IsSpawned();
        bool IsInactive();
        bool IsDespawned();
        bool IsTakeable();
        UniTask Initialize();
    }

    public class AshenBehaviour : MonoBehaviour, IPoolable
    {

        private int _id;

        private SpawnedObjectState _state = SpawnedObjectState.Inactive;

        public async UniTask Initialize()
        {
            await UniTask.CompletedTask;
        }

        public void SetId(int id) { _id = id; }

        public int GetId()
        {
            return _id;
        }

        public void OnSpawn()
        {
            _state = SpawnedObjectState.Spawned;
        }
        public void OnDespawn()
        {
            _state = SpawnedObjectState.Despawned;
        }
        public void OnTakenFromPool()
        {
            _state = SpawnedObjectState.Active;
        }
        public void OnReturnedToPool()
        {
            
            _state = SpawnedObjectState.Inactive;
        }

        public void SetState(SpawnedObjectState state)
        {
            _state = state;
        }

        public bool IsActive() { return _state == SpawnedObjectState.Active; }
        public bool IsSpawned() { return _state == SpawnedObjectState.Spawned; }
        public bool IsInactive() { return _state == SpawnedObjectState.Inactive; }
        public bool IsDespawned() { return _state == SpawnedObjectState.Despawned; }
        public bool IsTakeable() { return _state == SpawnedObjectState.Inactive || _state == SpawnedObjectState.Spawned; }



        public SpawnedObjectState GetState()
        {
            return _state;
        }


        [Inject]
        public void Construct(AshenCoreServices services)
        {

        }

    }
}