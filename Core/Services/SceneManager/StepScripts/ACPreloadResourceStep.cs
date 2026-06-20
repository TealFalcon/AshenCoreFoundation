using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Preload Resource Step")]
    public class ACPreloadResourceStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            foreach(string resource in sceneDefinition.SceneAssetNames)
            {
                sceneContext.debugSystem.Log("[ASHEN CORE] Loading Resource: " + resource, ConsoleMessageType.Verbose);

                AssetHandle resourceHandle = new AssetHandle();
                resourceHandle.Type = ResourceType.resourcekey;
                resourceHandle.Key = resource;

                await sceneContext.resourcesService.PreloadAsync(resourceHandle);
            }

            sceneContext.debugSystem.Log("[ASHEN CORE] Preload Resource Step Executed" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
            
        }


    }


}