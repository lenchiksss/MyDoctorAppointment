using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly ISerializationService serializationService;

        public DoctorRepository(string appSettingsPath, ISerializationService serializationService) : base(appSettingsPath, serializationService)
        {
            this.serializationService = serializationService;

            var result = ReadFromAppSettings(appSettingsPath);

            Path = result.Database.Doctors.Path;
            LastId = result.Database.Doctors.LastId;
        }

        public override string Path { get; set; }

        public override int LastId { get; set; }

        public override void ShowInfo(Doctor source)
        {
            Console.WriteLine();
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings(appSettings);

            result.Database.Doctors.LastId = LastId;

            serializationService.Serialize(appSettings, result);

            //File.WriteAllText(Constants.JsonAppSettingsPath, result.ToString());
        }
    }
}
