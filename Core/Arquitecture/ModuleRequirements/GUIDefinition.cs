using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace AshenCore.Core
{
    [CreateAssetMenu(menuName = "AshenCore/UI/GUI Definition")]
    public class GUIDefinition : ScriptableObject
    {
        public string Id;
        public GameObject Prefab;
        public AssetReference AssetRef;
        public bool IsSingleton;
        public bool BlockInput;
        public bool PauseGame;
        public ACUILayers Layer;
        public int SortingOrder;
    }
}