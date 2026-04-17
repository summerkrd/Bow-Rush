using System.Collections;
using UnityEngine;

namespace Develop.Runtime.Utilities.CoroutinesManagment
{
    public interface ICoroutinePerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}