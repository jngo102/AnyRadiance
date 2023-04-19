using System.Collections;

namespace HKAIFramework
{
    /// <summary>
    /// An interface for the base action class.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// A method that the base action class must implement; represents the logic that is executed within an action.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator Execute();
    }
}