using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Pool Step")]
    public class ACPoolStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            if (sceneDefinition.PoolsDefinitions == null || sceneDefinition.PoolsDefinitions.Length == 0)
            {
                sceneContext.debugSystem.Log("No pools defined in scene " + sceneDefinition.Alias, ConsoleMessageType.Info);
            }
            else
            {
                
                for (int i = 0; i < sceneDefinition.PoolsDefinitions.Length; i++)
                {
                    await sceneContext.spawnerService.PrewarmPool(sceneDefinition.PoolsDefinitions[i]);
                    sceneContext.debugSystem.Log("Prewarmed pool " + sceneDefinition.PoolsDefinitions[i].Id, ConsoleMessageType.Info);
                    await UniTask.Delay(1000);

                }
            }

        }


    }


}