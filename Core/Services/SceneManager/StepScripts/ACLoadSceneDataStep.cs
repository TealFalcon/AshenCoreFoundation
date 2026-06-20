using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Load Scene Data Step")]
    public class ACLoadSceneDataStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            await sceneContext.persistenceService.LoadSceneData(sceneContext.SceneManager.CurrentScene.SceneId);

            sceneContext.debugSystem.Log("[ASHEN CORE] Scene data Loaded" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
        }


    }


}