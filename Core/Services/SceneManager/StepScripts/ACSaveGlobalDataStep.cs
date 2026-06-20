using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Save Global Data Step")]
    public class ACSaveGlobalDataStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            if (sceneContext.persistenceService.GetCurrentSlot() == null)
            {
                sceneContext.debugSystem.Log("[ASHEN CORE] No slot selected", ConsoleMessageType.Error);
                return;
            }
            
            await sceneContext.persistenceService.SaveAsync(sceneContext.persistenceService.GetCurrentSlot().slotId);

            sceneContext.debugSystem.Log("[ASHEN CORE] Global data saved" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
        }


    }


}