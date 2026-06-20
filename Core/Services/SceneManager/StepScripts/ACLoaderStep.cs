using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{


    public class ACLoaderStep : ACLoadStep
    {

        public override UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            //TODO GET DEBUG SYSTEM AND SAVE LOGS VALUES
            return UniTask.CompletedTask;
        }


    }


}