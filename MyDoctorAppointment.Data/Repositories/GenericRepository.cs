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
using System.Xml;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public string AppSettings { get; private set; }
       
        public ISerializationService SerializationService { get; private set; }

        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public GenericRepository(string appSettings, ISerializationService serializationService)
        {
            AppSettings = appSettings;
            SerializationService = serializationService;
        }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            var doctors = GetAll().Append(source).ToList();

            SerializationService.Serialize(Path, doctors);

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

            //var file = File.ReadAllText(Path);

            //if (string.IsNullOrWhiteSpace(file))
            //{
            //    File.WriteAllText(Path, "[]");
            //    file = "[]";
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

        protected Repository ReadFromAppSettings()
        {
            return SerializationService.Deserialize<Repository>(AppSettings);
        }

        //protected dynamic ReadFromAppSettingsJson() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.JsonAppSettingsPath));
    }
}