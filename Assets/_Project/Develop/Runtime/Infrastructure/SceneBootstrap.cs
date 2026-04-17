using System.Collections;
using Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Develop.Runtime.Gameplay.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract IEnumerator Initialize(DIContainer container, IInputSceneArgs sceneArgs = null);

        public abstract void Run();
    }
}