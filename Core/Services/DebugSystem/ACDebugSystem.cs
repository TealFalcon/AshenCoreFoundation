using UnityEngine;
using VContainer;
using System.Collections.Generic;

namespace AshenCore.Core
{

    [System.Flags]
    public enum ConsoleMessageType
    {
        None        = 0,
        Verbose     = 1 << 0,
        Info        = 1 << 1,
        Warning     = 1 << 2,
        Error       = 1 << 3,
        Exception   = 1 << 4,
        Critical    = 1 << 5,
        Input       = 1 << 6
    }

    

    public class ACDebugSystem : MonoBehaviour, ILog
    {

        private ACSceneManager _sceneManager;
        public GUIConsoleController _controller;
        public GUIVariableDebug _variableDebug;
        public bool useGUI;
        public ConsoleMessageType filter;
        public List<string> registeredServices;
        public bool debugMode;
        public GameObject debugCamera;


        [Inject]
        void Construct(AshenCoreServices _services)
        {
            _sceneManager = _services.Scenes;
            _sceneManager.OnSceneChanged += OnSceneChanged;
        }

        void OnSceneChanged(ACSceneDefinition sceneDefinition)
        {
            if (sceneDefinition.SceneType == ACSceneType.Game)
            {
                debugCamera.SetActive(false);
            }
        }

        public void SetDebugCamera(GameObject camera) { debugCamera = camera; }


        public bool GetDebugMode() { return debugMode; }

        public void SetRegisteredServices(List<string> registeredServices)
        {
            this.registeredServices = registeredServices;
        }

        public List<string> GetRegisteredServices()
        {
            return registeredServices;
        }

        public void Log(string message)
        {
            if (_controller != null)
            {
                _controller.AddMessage(message);

                Debug.Log(message);
            }
        }

        public void Log(string message, ConsoleMessageType type)
        {
            if (_controller != null && filter.HasFlag(type))
            {
                _controller.AddMessage(message, type);
                Debug.Log(message);
            }
        }

        public void Warn(string message)
        {
            if (_controller != null && filter.HasFlag(ConsoleMessageType.Warning))
            {
                _controller.AddMessage(message);
                Debug.LogWarning(message);
            }
        }
        public void Error(string message)
        {
            if (_controller != null && filter.HasFlag(ConsoleMessageType.Error))
            {
                _controller.AddMessage(message);
                Debug.LogError(message);
            }
        }

        public void SetGUI(GUIConsoleController controller)
        {
            _controller = controller;
        }

        public async void InitializeGUI()
        {
            if(_sceneManager != null) await _sceneManager.LoadScene(2);
        }

        public void ClearGUI()
        {
            if (_controller != null) _controller.ClearMessages();
        }

        public bool UseGUI() { return useGUI; }

        public void SetVariable(string varName, string varValue)
        {
            if (_variableDebug != null) _variableDebug.SetVariable(varName, varValue);
        }
    }
}