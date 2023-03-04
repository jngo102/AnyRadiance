using System.Collections;

namespace HKAIFramework
{
    public abstract class Action : IAction
    {
        protected virtual string Name { get; }
        public abstract IEnumerator Execute();
    }
}