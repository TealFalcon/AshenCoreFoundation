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

            if (sceneContext.feedbackService != null)
                if(sceneContext.feedbackService.Screen != null)
                    if(sceneContext.feedbackService.Screen.Fade != null)
                    await sceneContext.feedbackService.Screen.Fade.FadeIn();
            else
                await UniTask.Delay(1000);
            
            await UniTask.Delay(1000);
            
        }


    }


}