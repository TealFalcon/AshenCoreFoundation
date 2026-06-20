using UnityEngine;
using System.Collections.Generic;

namespace AshenCore.Core
{

    public class UnityLogSystem : ILog
    {
        GUIConsoleController _controller;
        List<string> registeredServices;
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void SetRegisteredServices(List<string> registeredServices)
        {
            this.registeredServices = registeredServices;
        }

        public List<string> GetRegisteredServices()
        {
            return registeredServices;
        }

        public void Log(string message, ConsoleMessageType type)
        {
            if (_controller != null) _controller.AddMessage(message, type);
            switch (type)
            {
                case ConsoleMessageType.Verbose:
                case ConsoleMessageType.Info:
                    Debug.Log(message);
                    break;
                case ConsoleMessageType.Warning:
                    Debug.LogWarning(message);
                    break;
                case ConsoleMessageType.Error:
                case ConsoleMessageType.Exception:
                case ConsoleMessageType.Critical:
                    Debug.LogError(message);
                    break;
            }

        }

        public void Warn(string message)
        {
            Debug.LogWarning(message);
        }
        public void Error(string message)
        {
            Debug.LogError(message);
        }

        public void SetGUI(GUIConsoleController controller) { }
        public void InitializeGUI() { }
        public bool UseGUI() { return false; }
        public void ClearGUI() { }
        public void SetVariable(string varName, string varValue) { }
    }
}