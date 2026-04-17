using System;
using System.Collections;
using Develop.Runtime.Infrastructure.DI;
using Develop.Runtime.Utilities.CoroutinesManagment;
using Develop.Runtime.Gameplay.Infrastructure;
using UnityEngine;

namespace Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        
        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container  = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            Debug.Log($"Вы попали на уровень {gameplayInputArgs.LevelNumber}");
            
            Debug.Log("Инициализация геймплейной сцены");
            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт Геймплейной сцены");
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinePerformer coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
                coroutinePerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}