using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Debug Step")]
    public class ACDebugStep : ACLoadStep
    {

        public override UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            //TODO GET DEBUG SYSTEM AND SAVE LOGS VALUES

            sceneContext.debugSystem?.Log("[ASHEN CORE] Debug Step Executed" + " Scene: " + sceneDefinition.Alias, ConsoleMessageType.Info);
            
            return UniTask.CompletedTask;
        }


    }


}