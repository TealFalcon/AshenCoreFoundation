using UnityEngine;
using VContainer;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

namespace AshenCore.Core
{


    public class GUIConsoleController : MonoBehaviour
    {
        private ILog _debugSystem;
        private ACInputSystem _input;
        private ACSceneManager _sceneManager;
        private IACPersistenceSystem _persistenceSystem;

        public TMP_Text _text;
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] ScrollRect _scrollRect;
        public GameObject _canvas;

        private ConsoleCommandRegistry _commandRegistry;

        private string[] _inputHistory;
        private int _historyIndex = -1;

        public Color verboseColor = Color.white;
        public Color infoColor = Color.green;
        public Color warningColor = Color.yellow;
        public Color errorColor = Color.red;
        public Color exceptionColor = Color.magenta;
        public Color criticalColor = Color.red;
        public Color inputColor = Color.brown;

        public List<string> registeredCommands = new List<string>();

        [Inject]
        void Construct(AshenCoreServices services)
        {
            _debugSystem = services.Debug;

            if (_debugSystem.UseGUI())
            {
                _debugSystem.SetGUI(this);
            }

            _input = services.Input;
            _sceneManager = services.Scenes;
            _persistenceSystem = services.Persistence;
            _commandRegistry = new ConsoleCommandRegistry();


            AddMessage("Console Services started.");

            /* ******************************************************************************************

            *******                                 COMMAND REGISTRATION                          *******
            ********
            ****************************************************************************************** */

            _commandRegistry.Register(this,new CreateNewGameCommand(_persistenceSystem, _debugSystem));
            _commandRegistry.Register(this,new GetAllSavesCommand(_persistenceSystem, _debugSystem));
            _commandRegistry.Register(this,new DeleteSaveCommand(_persistenceSystem, _debugSystem));
            _commandRegistry.Register(this,new ClearConsoleCommand(_debugSystem));
            _commandRegistry.Register(this,new SaveCommand(_persistenceSystem, _debugSystem));
            _commandRegistry.Register(this,new LoadCommand(_persistenceSystem, _debugSystem));
            _commandRegistry.Register(this,new LoadSceneCommand(_sceneManager, _debugSystem));
            _commandRegistry.Register(this,new GetScenesCommand(_sceneManager, _debugSystem));
            _commandRegistry.Register(this,new ExitCommand());
            _commandRegistry.Register(this,new HelpCommand(this, _debugSystem));
            _commandRegistry.Register(this,new LoadSceneDataCommand(_persistenceSystem, _debugSystem, _sceneManager));
            _commandRegistry.Register(this,new SaveSceneCommand(_persistenceSystem, _debugSystem, _sceneManager));
            _commandRegistry.Register(this, new ListServicesCommand(_debugSystem));
            _commandRegistry.Register(this, new GaussCommand(_debugSystem));

        }

        void Start()
        {
            if(_input != null)
                _input.OnInput += InputEventHandler;
        }

        void InputEventHandler(ACInputEvent ie)
        {

            if (ie.Action == "OPEN_CONSOLE" && ie.Phase == ACInputPhase.Pressed)
            {
                _canvas.SetActive(!_canvas.activeSelf);

                if (_canvas.activeSelf)
                    FocusInput();
            }

            if(ie.Action == "UP" && ie.Phase == ACInputPhase.Pressed)
            {
                _historyIndex -= 1;
                _inputField.text = _inputHistory[_historyIndex];
            }

        }

        public void AddMessage(string message)
        {
            _text.text += message + "\n";
        }

        public void AddMessage(string message, ConsoleMessageType type)
        {
            string coloredMessage = $"<color=#{GetColorForType(type)}>{message}</color>";
            _text.text += coloredMessage + "\n";
        }

        private string GetColorForType(ConsoleMessageType type)
        {
            return type switch
            {
                ConsoleMessageType.Verbose => ColorUtility.ToHtmlStringRGBA(verboseColor),
                ConsoleMessageType.Info => ColorUtility.ToHtmlStringRGBA(infoColor),
                ConsoleMessageType.Warning => ColorUtility.ToHtmlStringRGBA(warningColor),
                ConsoleMessageType.Error => ColorUtility.ToHtmlStringRGBA(errorColor),
                ConsoleMessageType.Exception => ColorUtility.ToHtmlStringRGBA(exceptionColor),
                ConsoleMessageType.Critical => ColorUtility.ToHtmlStringRGBA(criticalColor),
                ConsoleMessageType.Input => ColorUtility.ToHtmlStringRGBA(inputColor),
                _ => ColorUtility.ToHtmlStringRGBA(Color.white),
            };
        }

        public void ClearMessages()
        {
            _text.text = "";
        }

        public async void ExecuteCommand()
        {
            string command = _inputField.text;

            AddMessage("> " + command);
            _inputHistory = _inputHistory.Append(command).ToArray();
            AddMessage(await _commandRegistry.Execute(command));

            _inputField.text = "";
            FocusInput();
            _scrollRect.verticalNormalizedPosition = -10f; // Scroll to bottom

        }

        private void FocusInput()
        {
            // EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
            _inputField.Select();
            _inputField.ActivateInputField();
        }

    }
    
}
