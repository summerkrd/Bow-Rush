using System;
using UnityEngine;

namespace Develop.Runtime.Gameplay.Infrastructure
{
    public class StandardLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        public bool IsShown => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
            // DontDestroyOnLoad(gameObject);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}