using System.IO;
using Backups.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Serializer
{
    public class Serializer<T>
    {
        private string _jsonFilePath;
        private T _serializeObject;

        private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };

        public Serializer(string jsonFilePath, T serializeObject)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath)) throw new BackupsException("Invalid path to json");
            if (serializeObject is null) throw new BackupsException("invalid object");
            _jsonFilePath = jsonFilePath;
            _serializeObject = serializeObject;
        }

        public void Save()
        {
            File.WriteAllText(
                _jsonFilePath,
                JsonConvert.SerializeObject(
                    _serializeObject, _serializerSettings));
        }

        public T Load()
        {
            return JsonConvert.DeserializeObject<T>(
                File.ReadAllText(_jsonFilePath),
                _serializerSettings);
        }
    }
}