using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Fade Out Step")]
    public class ACFadeOutStep : ACLoadStep
    {

        public override async UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;
            await UniTask.Delay(1000);

            if (sceneContext.fadeService != null)
                await sceneContext.fadeService.FadeOut();   
            else
                await UniTask.Delay(1000);
            
            
        }

    }


}