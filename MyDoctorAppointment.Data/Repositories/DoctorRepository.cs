﻿using MyDoctorAppointment.Data.Interfaces;
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
        private readonly ISerializationService _serializationService;

        public DoctorRepository(string appSettings, ISerializationService serializationService) : base(appSettings, serializationService)
        {
            dynamic result = ReadFromAppSettings();

            Path = result.Database.Doctors.Path;
            LastId = result.Database.Doctors.LastId;

            _serializationService = serializationService;
        }

        public override string Path { get; set; }

        public override int LastId { get; set; }

        public override void ShowInfo(Doctor doctor)
        {
            if (doctor != null)
            {
                Console.WriteLine("Id: " + doctor.Id + "; name: " + doctor.Name + "; surname: " + doctor.Surname
                + "; email: " + doctor.Email + "; phone: " + doctor.Phone
                + "; type: " + doctor.DoctorType + "; experiance: " + doctor.Experience);
            }
            else
            {
                throw new ArgumentNullException(nameof(doctor) + " can't be null");
            }
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;

            serializationService.Serialize(appSettings, result);

            //File.WriteAllText(Constants.JsonAppSettingsPath, result.ToString());
        }
    }
}