using System;
using System.Collections;
using System.Collections.Generic;

namespace Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public abstract class DataProvider<TData> where TData : ISaveData
    {
        private readonly ISaveLoadService _saveLoadService;
        
        private readonly List<IDataWriter<TData>> _writers = new();
        private readonly List<IDataReader<TData>> _readers = new();
        
        private TData _data;

        protected DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void RegisterWriter(IDataWriter<TData> writer)
        {
            if (_writers.Contains(writer))
                throw new ArgumentException($"The writer {nameof(writer)} is already registered");
            
            _writers.Add(writer);
        }
        
        public void RegisterReader(IDataReader<TData> reader)
        {
            if (_readers.Contains(reader))
                throw new ArgumentException($"The writer {nameof(reader)} is already registered");
            
            _readers.Add(reader);
        }

        public IEnumerator Load()
        {
            yield return _saveLoadService.Load<TData>(loadedData => _data = loadedData);
            
            SendDataToReaders();
        }

        public IEnumerator Save()
        {
            UpdateDataFromWriters();
            
            yield return _saveLoadService.Save(_data);
        }

        public IEnumerator Exists(Action<bool> onExist)
        {
            yield return _saveLoadService.Exists<TData>(result => onExist?.Invoke(result));
        }
        
        public void Reset()
        {
            _data = GetOriginalData();
            
            SendDataToReaders();
        }

        protected abstract TData GetOriginalData();

        private void SendDataToReaders()
        {
            foreach (IDataReader<TData> reader in _readers)
                reader.ReadFrom(_data);
        }

        private void UpdateDataFromWriters()
        {
            foreach (IDataWriter<TData> writer in _writers)
                writer.WriteTo(_data);
        }
    }
}