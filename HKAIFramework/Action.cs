using System.Collections;

namespace HKAIFramework
{
    /// <summary>
    /// An abstract class that custom actions may extend from.
    /// </summary>
    public abstract class Action : IAction
    {
        /// <summary>
        /// The name of the action.
        /// </summary>
        protected virtual string Name { get; }

        /// <summary>
        /// The action's code to execute.
        /// </summary>
        /// <returns>An IEnumerator that lasts the duration of the executed action</returns>
        public abstract IEnumerator Execute();
    }
}