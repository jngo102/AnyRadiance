using System.Collections;

namespace HKAIFramework
{
    public class ActionSequence
    {
        public string Name;

        public Action[] Actions;

        public Action CurrentAction { get; private set; }

        public static bool operator ==(ActionSequence a, ActionSequence b)
        {
            return a.Actions == b.Actions && a.Name == b.Name;
        }

        public static bool operator !=(ActionSequence a, ActionSequence b)
        {
            return a.Actions != b.Actions || a.Name != b.Name;
        }

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