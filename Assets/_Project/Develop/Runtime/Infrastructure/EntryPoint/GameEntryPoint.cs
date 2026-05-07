using System;
using System.Collections;
using Develop.Runtime.Infrastructure.DI;
using Develop.Runtime.Utilities.ConfigsManagment;
using Develop.Runtime.Utilities.CoroutinesManagment;
using Develop.Runtime.Utilities.DataManagment.DataProviders;
using Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Develop.Runtime.Utilities.SceneManagment
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Старт проекта, сетап настроек");
            SetupAppSettings();

            Debug.Log("Процесс регистрации сервисов всего проекта");
            DIContainer projectContainer = new DIContainer();
            ProjectContextRegistrations.Process(projectContainer);
            
            projectContainer.Initialize();

            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(Initialize(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();
            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();
            
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            Debug.Log("Открывается штора загрузки");
            loadingScreen.Show();

            Debug.Log("Начинается инициализация сервисов");
            yield return container.Resolve<ConfigsProviderService>().LoadAsync();

            bool isPlayerDataSaveExists = false;
            yield return playerDataProvider.Exists(result => isPlayerDataSaveExists = result);
            
            if (isPlayerDataSaveExists)
                yield return playerDataProvider.Load();
            else
                playerDataProvider.Reset();
            
            yield return new WaitForSeconds(1);

            Debug.Log("Завершается инициализация сервисов");

            Debug.Log("Закрывается штора загрузки");
            loadingScreen.Hide();
            
            Debug.Log("Начинается переход на какую то сцену");
            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}