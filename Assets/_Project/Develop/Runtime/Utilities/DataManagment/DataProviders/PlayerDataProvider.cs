using System;
using System.Collections.Generic;
using Develop.Runtime.Configs.Meta.Wallet;
using Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginalData()
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(),
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartWalletConfig walletConfig = _configsProviderService.GetConfig<StartWalletConfig>();

            if (walletConfig == null)
                Debug.Log("walletConfig is null");

            foreach (CurrencyTypes types in Enum.GetValues(typeof(CurrencyTypes)))
                walletData[types] = walletConfig.GetValueFor(types);

            return walletData;
        }
    }
}