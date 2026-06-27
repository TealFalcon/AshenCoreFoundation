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

    
}