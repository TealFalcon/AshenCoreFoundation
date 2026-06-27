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

            if (sceneContext.feedbackService != null)
                if (sceneContext.feedbackService.Screen != null)
                    if (sceneContext.feedbackService.Screen.Fade != null)
                        await sceneContext.feedbackService.Screen.Fade.FadeOut();   
            else
                await UniTask.Delay(1000);
            
            
        }

    }


}