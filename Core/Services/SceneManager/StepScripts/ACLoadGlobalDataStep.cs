using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Load Global Data Step")]
    public class ACLoadGlobalDataStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            if (sceneContext.persistenceService.GetCurrentSlot() == null)
            {
                sceneContext.debugSystem.Log("[ASHEN CORE] No slot selected", ConsoleMessageType.Error);
                return;
            }
            
            await sceneContext.persistenceService.LoadAsync(sceneContext.persistenceService.GetCurrentSlot().slotId);

            sceneContext.debugSystem.Log("[ASHEN CORE] Global data Loaded" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
        }


    }


}