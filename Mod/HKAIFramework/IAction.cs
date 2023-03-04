using UnityEngine;
using System.Collections;

namespace HKAIFramework
{
    public interface IAction
    {
        public abstract IEnumerator Execute();
    }
}