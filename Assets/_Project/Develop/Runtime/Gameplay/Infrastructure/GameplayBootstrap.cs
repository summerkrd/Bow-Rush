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
        private GameplayInputArgs _inputArgs;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container  = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");
            
            _inputArgs = gameplayInputArgs;
            
            GameplayContextRegistrations.Process(container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Вы попали на уровень {_inputArgs.LevelNumber}");
            
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