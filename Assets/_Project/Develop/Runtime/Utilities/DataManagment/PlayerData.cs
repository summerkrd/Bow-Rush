using System.Collections.Generic;

namespace Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}