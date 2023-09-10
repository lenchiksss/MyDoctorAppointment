using MyDoctorAppointment.Data.Interfaces;
using Newtonsoft.Json;

namespace MyDoctorAppointment.Service.Services
{
    public class JsonDataSerializerService : ISerializationService
    {
        public T Deserialize<T>(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Serialize<T>(string path, T data)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}