using System;
using System.Collections;
using Develop.Runtime.Utilities.DataManagment.DataRepository;
using Develop.Runtime.Utilities.DataManagment.KeysStorage;
using Develop.Runtime.Utilities.DataManagment.Serializers;

namespace Develop.Runtime.Utilities.DataManagment
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeysStorage _keysStorage;
        private readonly IDataRepository  _repository;

        public SaveLoadService(
            IDataSerializer serializer, 
            IDataKeysStorage keysStorage, 
            IDataRepository repository)
        {
            _serializer = serializer;
            _keysStorage = keysStorage;
            _repository = repository;
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();
            string serializedData = "";
            
            yield return _repository.Read(key, result => serializedData = result);
            
            TData data = _serializer.Deserialize<TData>(serializedData);
            
            onLoad?.Invoke(data);
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _repository.Write(key, serializedData);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _repository.Remove(key);
        }

        public IEnumerator Exists<TData>(Action<bool> onExist) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();
            
            yield return _repository.Exist(key, result => onExist?.Invoke(result));
        }
    }
}