using System.Collections;
using System.Collections.Generic;
using Develop.Runtime.Infrastructure.DI;
using Develop.Runtime.Utilities.DataManagment;
using Develop.Runtime.Utilities.CoroutinesManagment;
using Develop.Runtime.Utilities.DataManagment.Serializers;
using UnityEngine;

namespace Develop.Runtime.Utilities.SceneManagment
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        
        private WalletService _walletService;
        
        private PlayerData _playerData;
        private IDataSerializer _serializer;
        private string _serializedPlayerData;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container  = container;
            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация меню сцены");
            
            _walletService = _container.Resolve<WalletService>();

            _playerData = new PlayerData();
            _playerData.WalletData = new Dictionary<CurrencyTypes, int>()
            {
                { CurrencyTypes.Gold, 10 },
                { CurrencyTypes.Diamond, 150 },
            };

            _serializer = new JsonSerializer();
            
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

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
               _serializedPlayerData = _serializer.Serialize(_playerData);
               Debug.Log(_serializedPlayerData);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlayerData playerData = _serializer.Deserialize<PlayerData>(_serializedPlayerData);
                Debug.Log("Золото в данных: " + playerData.WalletData[CurrencyTypes.Gold]);
                Debug.Log("Алмазы в данных: " + playerData.WalletData[CurrencyTypes.Diamond]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log("ТЕСТ " + Time.frameCount);
            }
        }
    }
}