using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AddressableAssets;

namespace AshenCore.Core
{
    [Serializable]
    public class ACSpawnHandle
    {
        public int Id;
        public GameObject Instance;

        public ACSpawnHandle(int id, GameObject instance)
        {
            Id = id;
            Instance = instance;
        }
    }

    public struct AssetHandle
    {

        public ResourceType Type;
        public string Key;
        public AssetReference AssetReference;
        public UnityEngine.Object AssetObject;

    }

    public enum ResourceType
    {
        assetobject,
        reference,
        resourcekey
    }

    public class ACSpawnRequest
    {
        public int PoolId;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform parent;
        public bool ActivateOnSpawn;

        public ACSpawnRequest(int poolId, Vector3 position, Quaternion rotation, Transform parent, bool activateOnSpawn)
        {
            PoolId = poolId;
            Position = position;
            Rotation = rotation;
            this.parent = parent;
            ActivateOnSpawn = activateOnSpawn;
        }
    }

    [Serializable]
    public class GUIHelper
    {

        public GUIDefinition definition;
        public ACWindowState state = ACWindowState.Destroyed;
        public ACUIWindow windowComponent;

        public GUIHelper(GUIDefinition definition)
        {
            this.definition = definition;
        }

    }


    [Serializable]
    public class ACIconDefinition
    {
        public string Id;

        public Sprite image;

        public Vector2 DefaultSize = new(24, 24);

        public bool PreserveAspect = true;
    }

    [Serializable]    
    public class ACPool
    {
        public int Id;
        public GameObject Prefab;
        public List<ACSpawnHandle> GameObjects;
    }


}