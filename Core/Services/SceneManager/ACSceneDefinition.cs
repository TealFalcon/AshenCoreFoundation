using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AshenCore.Core
{
    [CreateAssetMenu(fileName = "SceneDefinition", menuName = "AshenCore/Scene Definition")]
        public class ACSceneDefinition : ScriptableObject
        {
            public int SceneId;
            public string Alias;
            public AssetReference SceneAsset;
            public string[] SceneAssetNames;
            public ACSceneType SceneType;
            public ACScenePipeline ScenePipeline;
            public ACScenePipeline FastPipeline;
            public ACPoolDefinition[] PoolsDefinitions;
        }
    
}