using System;
using System.Collections;

namespace Develop.Runtime.Utilities.DataManagment
{
    public interface ISaveLoadService
    {
        IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData;
        
        IEnumerator Save<TData>(TData data) where TData : ISaveData;
        
        IEnumerator Remove<TData>() where TData : ISaveData;
        
        IEnumerator Exists<TData>(Action<bool> onExist) where TData : ISaveData;
    }
}