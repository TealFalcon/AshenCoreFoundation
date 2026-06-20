using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Save Scene Data Step")]
    public class ACSaveSceneDataStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            await sceneContext.persistenceService.SaveSceneData(sceneContext.SceneManager.CurrentScene.SceneId);

            sceneContext.debugSystem.Log("[ASHEN CORE] Scene data Saved" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
        }


    }


}