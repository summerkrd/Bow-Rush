using System;
using System.Collections.Generic;
using Develop.Runtime.Infrastructure.DI;
using Develop.Runtime.Utilities.DataManagment;
using Develop.Runtime.Utilities.AssetsManagment;
using Develop.Runtime.Utilities.ConfigsManagment;
using Develop.Runtime.Utilities.CoroutinesManagment;
using Develop.Runtime.Utilities.Reactive;
using Object = UnityEngine.Object;

namespace Develop.Runtime.Utilities.SceneManagment
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(CreateCoroutinesPerformer);
            container.RegisterAsSingle(CreateConfigsProviderService);
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
            container.RegisterAsSingle(CreateWalletService);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes types in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[types] = new ReactiveVariable<int>();

            return new WalletService(currencies);
        }

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c) => new();
        
        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
        {
            SceneLoaderService sceneLoaderService = c.Resolve<SceneLoaderService>();
            ILoadingScreen loadingScreen = c.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = new(sceneLoaderService, loadingScreen, c);
            return sceneSwitcherService;
        }
        
        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);
        
            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
        
            CoroutinesPerformer coroutinePerformerPrefab = resourcesAssetsLoader.
                Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");
        
            return Object.Instantiate(coroutinePerformerPrefab);
        }

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            
            StandardLoadingScreen loadingScreenPrefab = resourcesAssetsLoader.
                Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");
            
            return Object.Instantiate(loadingScreenPrefab);
        }
    
        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new();
    }
}