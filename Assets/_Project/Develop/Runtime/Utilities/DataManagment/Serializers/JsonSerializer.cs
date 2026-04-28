using Newtonsoft.Json;

namespace Develop.Runtime.Utilities.DataManagment.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        public string Serialize<TData>(TData data) => 
            JsonConvert.SerializeObject(data);
        

        public TData Deserialize<TData>(string serializedData) =>
            JsonConvert.DeserializeObject<TData>(serializedData);
    }
}