using System.IO;
using Backups.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Serializer
{
    public class MainSerializer<T>
    {
        private string _jsonFIlePath;
        private T _serializeObject;

        private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };

        public MainSerializer(string jsonFIlePath, T serializeObject)
        {
            if (string.IsNullOrWhiteSpace(jsonFIlePath)) throw new BackupsException("Invalid path to json");
            if (serializeObject is null) throw new BackupsException("invalid object");
            _jsonFIlePath = jsonFIlePath;
            _serializeObject = serializeObject;
        }

        public void Save()
        {
            File.WriteAllText(
                _jsonFIlePath,
                JsonConvert.SerializeObject(
                    _serializeObject, _serializerSettings));
        }

        public T Load()
        {
            return JsonConvert.DeserializeObject<T>(
                File.ReadAllText(_jsonFIlePath),
                _serializerSettings);
        }
    }
}