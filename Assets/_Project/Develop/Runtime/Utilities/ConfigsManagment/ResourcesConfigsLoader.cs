using System;
using System.Collections;
using System.Collections.Generic;
using Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPath = new()
        {
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (var configResourcesPath in _configsResourcesPath)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
                loadedConfigs.Add(configResourcesPath.Key, config);
                yield return null;
            }

            onConfigLoaded?.Invoke(loadedConfigs);
        }
    }
}