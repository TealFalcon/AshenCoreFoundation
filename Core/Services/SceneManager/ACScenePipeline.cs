using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(fileName = "ScenePipeline", menuName = "AshenCore/Scene Pipeline")]
    public class ACScenePipeline : ScriptableObject
    {
        
        public ACLoadStep[] _steps;
    }

    public abstract class ACLoadStep : ScriptableObject
    {
        public abstract UniTask Execute(ACSceneDefinition sceneDefinition,ACSceneContext sceneContext);
        public float Weight = 1f;
    }
}