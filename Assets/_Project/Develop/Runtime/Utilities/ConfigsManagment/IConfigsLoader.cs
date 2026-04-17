using System;
using System.Collections;
using System.Collections.Generic;

namespace Develop.Runtime.Utilities.ConfigsManagment
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded);
    }
}