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

        public ISerializationService serializationService { get; private set; }

        public GenericRepository(string appSettings, ISerializationService serializationService)
        {
            this.appSettings = appSettings;
            this.serializationService = serializationService;
        }

        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            var data = GetAll().Append(source).ToList();
            serializationService.Serialize(data, Path);

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

            var data = GetAll().Where(x => x.Id != id).ToList();
            serializationService.Serialize(data, Path);

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != id), Formatting.Indented));

            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            if (!File.Exists(Path))
            {
                return new List<TSource>();
            }

            return serializationService.Deserialize<List<TSource>>(Path) ?? new List<TSource>();

            //if (!File.Exists(Path))
            //{
            //    File.WriteAllText(Path, "[]");
            //}

            //return serializationService.Deserialize(Path);

            //var json = File.ReadAllText(Path);

            //if (string.IsNullOrWhiteSpace(json))
            //{
            //    File.WriteAllText(Path, "[]");
            //    json = "[]";
            //}

            //return JsonConvert.DeserializeObject<List<TSource>>(json)!;
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            var data = GetAll().Select(x => x.Id == id ? source : x).ToList();
            serializationService.Serialize(data, Path);

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Select(x => x.Id == id ? source : x), Formatting.Indented));

            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.JsonAppSettingsPath));
    }
}
