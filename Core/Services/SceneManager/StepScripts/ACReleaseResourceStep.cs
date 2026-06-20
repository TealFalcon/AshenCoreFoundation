using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Release Resource Step")]
    public class ACReleaseResourceStep : ACLoadStep
    {

        public override UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            foreach(string resource in sceneDefinition.SceneAssetNames)
            {
                sceneContext.debugSystem.Log("[ASHEN CORE] Releasing Resource: " + resource, ConsoleMessageType.Verbose);

                AssetHandle resourceHandle = new AssetHandle();
                resourceHandle.Type = ResourceType.resourcekey;
                resourceHandle.Key = resource;

                sceneContext.resourcesService.UnloadAsset(resourceHandle);
            }

            sceneContext.debugSystem.Log("[ASHEN CORE] Preload Resource Step Executed" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
            return UniTask.CompletedTask;
        }


    }


}