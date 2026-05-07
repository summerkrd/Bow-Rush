using System.Collections;
using System.Collections.Generic;
using Develop.Runtime.Infrastructure.DI;
using Develop.Runtime.Utilities.DataManagment;
using Develop.Runtime.Utilities.CoroutinesManagment;
using Develop.Runtime.Utilities.DataManagment.DataProviders;
using UnityEngine;

namespace Develop.Runtime.Utilities.SceneManagment
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        
        private WalletService _walletService;
        
        private PlayerDataProvider _playerDataProvider;
        
        private ICoroutinePerformer _coroutinePerformer;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container  = container;
            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация меню сцены");
            
            _walletService = _container.Resolve<WalletService>();
            
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
            
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

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log("Gold count " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log("Gold count " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinePerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("Сохранение было вызвано");
            }
        }
    }
}