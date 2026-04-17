using System;
using System.Collections;
using UnityEngine;

namespace Develop.Runtime.Utilities.CoroutinesManagment
{
    public class CoroutinesPerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartPerform(IEnumerator coroutineFunction)
             =>  StartCoroutine(coroutineFunction);
        
        public void StopPerform(Coroutine coroutine)
        => StopCoroutine(coroutine);
    }
}