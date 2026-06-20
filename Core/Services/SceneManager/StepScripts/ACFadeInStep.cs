using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Fade In Step")]
    public class ACFadeInStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            if (sceneContext.fadeService != null)
                await sceneContext.fadeService.FadeIn();
            else
                await UniTask.Delay(1000);
            
            await UniTask.Delay(1000);
            
        }


    }


}