using Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Develop.Runtime.Utilities.SceneManagment
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене Main menu");
        }
    }
}