using System;
using System.Collections;
using Develop.Runtime.Infrastructure.DI;
using Develop.Runtime.Utilities.CoroutinesManagment;
using Develop.Runtime.Gameplay.Infrastructure;
using UnityEngine;

namespace Develop.Runtime.Gameplay.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        
        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container  = container;
            
            Debug.Log("Инициализация меню сцены");
            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт меню сцены");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinePerformer coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
                coroutinePerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(2)));
            }
        }
    }
}