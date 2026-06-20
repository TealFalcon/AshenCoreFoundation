using UnityEngine;
using VContainer;
using System.Collections.Generic;

namespace AshenCore.Core
{

/*     public enum ConsoleMessageType
    {
        Verbose,
        Info,
        Warning,
        Error,
        Exception,
        Critical,
        Input
    } */

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

    public interface ILog
    {
        void Log(string message);
        void Log(string message, ConsoleMessageType type);
        void Warn(string message);
        void Error(string message);
        void SetGUI(GUIConsoleController controller);
        void InitializeGUI();
        bool UseGUI();
        void ClearGUI();
        void SetVariable(string varName, string varValue);
        void SetRegisteredServices(List<string> registeredServices);
        List<string> GetRegisteredServices();
    }

    public class ACDebugSystem : MonoBehaviour, ILog
    {

        private ACSceneManager _sceneManager;
        public GUIConsoleController _controller;
        public GUIVariableDebug _variableDebug;
        public bool useGUI;
        public ConsoleMessageType filter;
        public List<string> registeredServices;


        [Inject]
        void Construct(AshenCoreServices _services)
        {
            _sceneManager = _services.Scenes;
        }

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