using Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Develop.Runtime.Utilities.SceneManagment
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");
        }
    }
}