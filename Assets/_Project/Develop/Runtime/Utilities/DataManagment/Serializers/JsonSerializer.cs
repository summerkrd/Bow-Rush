using Newtonsoft.Json;

namespace Develop.Runtime.Utilities.DataManagment.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            });
        }


        public TData Deserialize<TData>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }
}