using System.Collections;

namespace HKAIFramework
{
    public class ActionSequence
    {
        /// <summary>
        /// The name of the action sequence.
        /// </summary>
        public string Name;

        /// <summary>
        /// An array of actions that will be executed in order.
        /// </summary>
        public Action[] Actions;

        /// <summary>
        /// The current action being executed.
        /// </summary>
        public Action CurrentAction { get; private set; }

        public static bool operator ==(ActionSequence a, ActionSequence b)
        {
            return a.Actions == b.Actions && a.Name == b.Name;
        }

        public static bool operator !=(ActionSequence a, ActionSequence b)
        {
            return a.Actions != b.Actions || a.Name != b.Name;
        }

        /// <summary>
        /// Execute the sequence of actions.
        /// </summary>
        /// <returns>An IEnumerator that lasts the duration of all actions executed</returns>
        public IEnumerator Execute()
        {
            foreach (var action in Actions)
            {
                CurrentAction = action;
                yield return action.Execute();
            }   
        }
    }
}