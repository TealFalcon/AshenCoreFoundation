using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{

    [CreateAssetMenu(menuName = "AshenCore/Scene Pipeline Steps/Load Additive Step")]
    public class ACLoadAdditiveStep : ACLoadStep
    {

        public async override UniTask Execute(ACSceneDefinition sceneDefinition, ACSceneContext sceneContext)
        {
            Weight = 0f;

            if (sceneContext.SceneManager == null)
                return;
                
            sceneContext.debugSystem?.Log("[ASHEN CORE] Loading Additive Scene: " + sceneDefinition.Alias,ConsoleMessageType.Info);

            IACSceneHandle handle;

            if(sceneContext.SceneManager.CurrentScene != null)
            {
                if (sceneDefinition.SceneType == ACSceneType.Game && sceneContext.SceneManager.CurrentScene.SceneType == ACSceneType.Game)
                {
                    sceneContext.SceneManager.CurrentScene = sceneDefinition;
                    sceneContext.SceneManager.LastScene = sceneContext.SceneManager.CurrentScene;

                    if (sceneContext.SceneManager.CurrentSceneHandle != null && sceneContext.SceneManager.CurrentScene != null)
                    {
                        if (sceneContext.SceneManager.CurrentSceneHandle is AddressableSceneHandle addressableSceneHandle)
                        {
                            await addressableSceneHandle.Unload();
                        }

                        if (sceneContext.SceneManager.CurrentSceneHandle is BuiltInSceneHandle builtInSceneHandle)
                        {
                            await builtInSceneHandle.Unload();
                        }
                    }
                }
                
            }

            if (sceneDefinition.SceneAsset.RuntimeKeyIsValid())
            {
                sceneContext.debugSystem.Log("[ASHEN CORE] Addressable Scene " + sceneDefinition.Alias + " loading...", ConsoleMessageType.Verbose);

                var op = Addressables.LoadSceneAsync(sceneDefinition.SceneAsset, LoadSceneMode.Additive);

                await op.ToUniTask();

                handle = new AddressableSceneHandle(op);
            }
            else
            {
                sceneContext.debugSystem.Log("[ASHEN CORE] Built-in Scene " + sceneDefinition.Alias + " loading...", ConsoleMessageType.Verbose);

                var op = SceneManager.LoadSceneAsync(sceneDefinition.SceneId, LoadSceneMode.Additive);

                await op.ToUniTask();

                var scene = SceneManager.GetSceneByBuildIndex(sceneDefinition.SceneId);

                handle = new BuiltInSceneHandle(scene);
            }

            sceneContext.debugSystem.SetVariable("CurrentScene", sceneDefinition.Alias);
            sceneContext.SceneManager.LastScene = sceneContext.SceneManager.CurrentScene;
            sceneContext.SceneManager.CurrentScene = sceneDefinition;
            sceneContext.SceneManager.CurrentSceneHandle = handle;
        }

    }

}