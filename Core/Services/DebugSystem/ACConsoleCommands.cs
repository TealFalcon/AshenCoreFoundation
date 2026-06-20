using UnityEngine;
using VContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace AshenCore.Core
{


    public interface IConsoleCommand
    {
        string Command { get; }

        UniTask<string> Execute(string[] args);
    }


    public class ConsoleCommandRegistry
    {
        private readonly Dictionary<string, IConsoleCommand> commands
            = new();

        public void Register(GUIConsoleController console,IConsoleCommand command)
        {
            commands[command.Command.ToLower()] = command;
            console.registeredCommands.Add(command.Command);
        }

        public async UniTask<string> Execute(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Empty command.";

            input = input.TrimStart('/');

            string[] parts = input.Split(' ');

            string commandName = parts[0].ToLower();

            if (!commands.TryGetValue(commandName, out var command))
                return $"Unknown command: {commandName}";

            string[] args = parts.Skip(1).ToArray();

            return await command.Execute(args);
        }
    }

    /* **************************************************************************************************************************************************
    *******************
    *****************                                                  COMMANDS REGISTRATATION
    **************                      
    ********
    *****
    ***
    ***************************************************************************************************************************************************** */

    public class CreateNewGameCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;

        public string Command => "CreateNewGame";

        public CreateNewGameCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem)
        {
            this._persistenceService = persistenceService;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Creating new game...");
            ACSlot newSlot = await _persistenceService.CreateNewSave();
            Log.Log($"Game created in slot {newSlot.slotId}");
            await _persistenceService.SaveAsync(newSlot.slotId);

            return $"Slot {newSlot.slotId} saved.";
        }
    }

    public class GetAllSavesCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;

        public string Command => "GetAllSaves";

        public GetAllSavesCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem)
        {
            this._persistenceService = persistenceService;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Getting all saves...");
            var saves = _persistenceService.GetAllSaves();

            foreach (var save in saves)
            {
                Log.Log($"Slot: {save.slot}, Date: {save.saveDate}");
            }
            await UniTask.CompletedTask;
            return $"Found {saves.Count} saves.";
        }
    }

    public class DeleteSaveCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;

        public string Command => "DeleteSave";

        public DeleteSaveCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem)
        {
            _persistenceService = persistenceService;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Removing Save..." + args[0]);
            bool result = await _persistenceService.DeleteAsync(int.Parse(args[0]));
            return result ? $"Save deleted successfully." : $"Failed to delete save.";
        }
    }


    public class SaveCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;

        public string Command => "SaveSlot";

        public SaveCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem)
        {
            _persistenceService = persistenceService;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Saving slot..." + args[0]);
            SaveResult result = await _persistenceService.SaveAsync(int.Parse(args[0]));
            return result.ToString();
        }
    }


    public class LoadCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;

        public string Command => "LoadSlot";

        public LoadCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem)
        {
            _persistenceService = persistenceService;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Loading slot..." + args[0]);
            LoadResult result = await _persistenceService.LoadAsync(int.Parse(args[0]));
            return result.ToString();
        }
    }

    public class SaveSceneCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;
        private readonly ACSceneManager _sceneManager;

        public string Command => "SaveScene";

        public SaveSceneCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem,
            ACSceneManager sceneManager)
        {
            _persistenceService = persistenceService;
            Log = debugSystem;
            _sceneManager = sceneManager;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Saving Scene...");

            SaveResult result = SaveResult.UnknownError;

            if (_sceneManager.CurrentScene != null)
            {
                result = await _persistenceService.SaveSceneData(_sceneManager.CurrentScene.SceneId);
            }
            else
            {
                result = SaveResult.Failed;
                Log.Log("You are not in a scene.", ConsoleMessageType.Error);
            }

            return result.ToString();
        }
    }


    public class LoadSceneDataCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;
        private readonly ACSceneManager _sceneManager;


        public string Command => "LoadSceneData";

        public LoadSceneDataCommand(
            IACPersistenceSystem persistenceService,
            ILog debugSystem,
            ACSceneManager sceneManager)
        {
            _persistenceService = persistenceService;
            Log = debugSystem;
            _sceneManager = sceneManager;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Loading Scene...");

            LoadResult result = LoadResult.UnknownError;

            if (_sceneManager.CurrentScene != null)
            {
                result = await _persistenceService.LoadSceneData(_sceneManager.CurrentScene.SceneId);
            }
            else
            {
                result = LoadResult.Failed;
                Log.Log("You are not in a scene.", ConsoleMessageType.Error);
            }
            return result.ToString();
        }
    }

    public class ClearConsoleCommand : IConsoleCommand
    {
        private readonly IACPersistenceSystem _persistenceService;
        private readonly ILog Log;

        public string Command => "clear";

        public ClearConsoleCommand(
            ILog debugSystem)
        {
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.ClearGUI();
            await UniTask.CompletedTask;
            return "";
        }
    }

    public class LoadSceneCommand : IConsoleCommand
    {
        private readonly ACSceneManager _sceneManager;
        private readonly ILog Log;

        public string Command => "loadscene";

        public LoadSceneCommand(
            ACSceneManager sceneManager,
            ILog debugSystem)
        {
            _sceneManager = sceneManager;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            await _sceneManager.LoadScene(int.Parse(args[0]));
            return "";
        }
    }

    public class GetScenesCommand : IConsoleCommand
    {
        private readonly ACSceneManager _sceneManager;
        private readonly ILog Log;

        public string Command => "getscenes";

        public GetScenesCommand(
            ACSceneManager sceneManager,
            ILog debugSystem)
        {
            _sceneManager = sceneManager;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            foreach (var scene in _sceneManager.GetSceneDefinitions())
            {
                Log.Log($"Scene ID: {scene.SceneId}, Name: {scene.Alias}", ConsoleMessageType.Info);
            }
            await UniTask.CompletedTask;
            return "";
        }
    }

    public class ExitCommand : IConsoleCommand
    {

        public string Command => "exit";


        public async UniTask<string> Execute(string[] args)
        {
            Application.Quit();
            await UniTask.CompletedTask;
            return "";
        }
    }

    public class ListServicesCommand : IConsoleCommand
    {

        public string Command => "listServices";
        private readonly ILog Log;

        public ListServicesCommand(
            ILog debugSystem)
        {
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Registered Services:");

            foreach (string service in Log.GetRegisteredServices())
            {
                Log.Log(service);
            }
            await UniTask.CompletedTask;
            return "";
        }
    }

    public class HelpCommand : IConsoleCommand
    {

        public string Command => "help";
        private readonly GUIConsoleController console;
        private readonly ILog Log;

        public HelpCommand(
            GUIConsoleController console,
            ILog debugSystem)
        {
            this.console = console;
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            Log.Log("Commands:");

            foreach (string c in console.registeredCommands)
            {
                Log.Log(c);
            }
            await UniTask.CompletedTask;
            return "";
        }
    }
    
    public class GaussCommand : IConsoleCommand
    {

        public string Command => "gauss";
        private readonly ILog Log;

        public GaussCommand(
            ILog debugSystem)
        {
            Log = debugSystem;
        }

        public async UniTask<string> Execute(string[] args)
        {
            float soc = ACMath.RandomGaussian(float.Parse(args[0]), float.Parse(args[1]));
            float act = ACMath.RandomGaussian(float.Parse(args[0]), float.Parse(args[1]));
            float reb = ACMath.RandomGaussian(float.Parse(args[0]), float.Parse(args[1]));
            float sen = ACMath.RandomGaussian(float.Parse(args[0]), float.Parse(args[1]));


            Log.Log("Sociable/Reservado" + soc.ToString(), ConsoleMessageType.Info);
            Log.Log("Activo/Pasivo" + act.ToString(), ConsoleMessageType.Info);
            Log.Log("Rebelde/Obediente" + reb.ToString(), ConsoleMessageType.Info);
            Log.Log("Sensible/Sereno" + sen.ToString(), ConsoleMessageType.Info);

            float media = (soc + act + reb + sen) / 4;
            Log.Log("MEDIA"+ media.ToString(), ConsoleMessageType.Info);

            await UniTask.CompletedTask;
            return "Bicho listo + " + soc.ToString() + " + " + act.ToString() + " + " + reb.ToString() + " + " + sen.ToString() + " = "+ media.ToString();
        }
    }


}