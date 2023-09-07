using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public string appSettings { get; private set; }

        public ISerializationService SerializationService { get; private set; }

        public GenericRepository(string appSettings, ISerializationService serializationService)
        {
            this.appSettings = appSettings;
            SerializationService = serializationService;
        }

        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            SerializationService.Serialize(Path, GetAll().Append(source));

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Append(source), Formatting.Indented));
            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
            {
                return false;
            }

            SerializationService.Serialize(Path, GetAll().Where(x => x.Id != id));

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != id), Formatting.Indented));

            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            //if (!File.Exists(Path))
            //{
            //    return new List<TSource>();
            //}

            //if (!File.Exists(Path))
            //{
            //    File.WriteAllText(Path, "[]");
            //}

            //var json = File.ReadAllText(Path);

            //if (string.IsNullOrWhiteSpace(json))
            //{
            //    File.WriteAllText(Path, "[]");
            //    json = "[]";
            //}

            //return serializationService.Deserialize<List<TSource>>(Path) ?? new List<TSource>();

            //return JsonConvert.DeserializeObject<List<TSource>>(json)!;

            return SerializationService.Deserialize<List<TSource>>(Path);
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            //var data = GetAll().Select(x => x.Id == id ? source : x).ToList();
            //serializationService.Serialize(data, Path);

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Select(x => x.Id == id ? source : x), Formatting.Indented));

            SerializationService.Serialize(Path, GetAll().Select(x => x.Id == id ? source : x));

            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings(string appSettingsPath)
        {
            try
            {
                string jsonOrXml = File.ReadAllText(appSettingsPath);

                if (appSettingsPath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    return SerializationService.Deserialize<dynamic>(appSettingsPath);
                }
                else if (appSettingsPath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    return SerializationService.Deserialize<dynamic>(appSettingsPath);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported file format. Only JSON and XML are supported.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine("Error reading app settings: " + ex.Message);
                return null; // Or throw an exception or return a default value
            }
        }

        //protected dynamic ReadFromAppSettings() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.XmlAppSettingsPath));
    }
}