using System.Collections;
using Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Develop.Runtime.Gameplay.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null);
        
        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
}