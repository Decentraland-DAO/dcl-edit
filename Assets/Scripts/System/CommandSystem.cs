using Assets.Scripts.EditorState;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.System
{
    public class CommandSystem : ICommandSystem
    {
        // dependencies
        public CommandFactorySystem CommandFactory { get; private set; }

        [Inject]
        public void Construct(CommandFactorySystem commandFactory)
        {
            CommandFactory = commandFactory;
        }

        public void ExecuteCommand<T>(T command) where T : SceneState.Command
        {
            if (command == null)
                return;

            var commandState = EditorStates.CurrentSceneState.CurrentScene?.CommandHistoryState;

            if (commandState == null)
            {
                Debug.LogError("No Scene State found");
                return;
            }

            if (commandState.CommandHistory.Count > commandState.CurrentCommandIndex + 1)
            {
                var firstDeprecatedCommandIndex = commandState.CurrentCommandIndex + 1;
                commandState.CommandHistory.RemoveRange(firstDeprecatedCommandIndex, commandState.CommandHistory.Count - firstDeprecatedCommandIndex);
            }

            commandState.CommandHistory.Add(command);
            commandState.CurrentCommandIndex = commandState.CommandHistory.Count - 1;

            command.Do(EditorStates.CurrentSceneState.CurrentScene);
        }

        public void UndoCommand()
        {
            var commandState = EditorStates.CurrentSceneState.CurrentScene?.CommandHistoryState;

            if (commandState == null)
            {
                Debug.LogError("No Scene State found");
                return;
            }

            if (commandState.CurrentCommandIndex >= 0)
            {
                commandState.CommandHistory[commandState.CurrentCommandIndex].Undo(EditorStates.CurrentSceneState.CurrentScene);
                commandState.CurrentCommandIndex--;
            }
        }

        public void RedoCommand()
        {
            var commandState = EditorStates.CurrentSceneState.CurrentScene?.CommandHistoryState;

            if (commandState == null)
            {
                Debug.LogError("No Scene State found");
                return;
            }

            if (commandState.CurrentCommandIndex < commandState.CommandHistory.Count - 1)
            {
                commandState.CurrentCommandIndex++;
                commandState.CommandHistory[commandState.CurrentCommandIndex].Do(EditorStates.CurrentSceneState.CurrentScene);
            }
        }
    }
}