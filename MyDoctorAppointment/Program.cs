using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService doctorService;

        public DoctorAppointment(string appSettings, ISerializationService serializationService)
        {
            doctorService = new DoctorService(appSettings, serializationService);
        }

        public void Menu()
        {
            Console.WriteLine("Current doctors list:");
            var docs = doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }

            Console.WriteLine("Adding new doctor:");
            var newDoctor = new Doctor
            {
                Name = "Vasya",
                Surname = "Petrov",
                Experience = 20,
                DoctorType = Domain.Enums.DoctorTypes.Dentist
            };

            doctorService.Create(newDoctor);

            Console.WriteLine("Current doctors list:");
            docs = doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }

        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Select data format:");
            Console.WriteLine("1. XML");
            Console.WriteLine("2. JSON");

            //string choice = Console.ReadLine();
            ////ISerializationService serializationService = null;
            ////string appSettingsPath = "";

            ////ISerializationService serializationService;
            //DoctorAppointment doctorAppointment = null;

            //if (choice.Equals("1"))
            //{
            //    //serializationService = new XmlDataSerializerService();
            //    //appSettingsPath = Constants.XmlAppSettingsPath;
            //    doctorAppointment = new DoctorAppointment(Constants.XmlAppSettingsPath, new XmlDataSerializerService());
            //}
            //else if (choice.Equals("2"))
            //{
            //    //serializationService = new JsonDataSerializerService();
            //    //appSettingsPath = Constants.JsonAppSettingsPath;
            //    doctorAppointment = new DoctorAppointment(Constants.JsonAppSettingsPath, new JsonDataSerializerService());
            //}
            //else
            //{
            //    Console.WriteLine("Invalid input");
            //    return;
            //}

            ////DoctorAppointment doctorAppointment = new DoctorAppointment(appSettingsPath, serializationService);
            //doctorAppointment.Menu();

            DoctorAppointment? doctorAppointment = null;

            string? choice = Console.ReadLine();
            try
            {
                switch (choice)
                {

                    case "1":
                        {
                            doctorAppointment = new DoctorAppointment(Constants.XmlAppSettingsPath, new XmlDataSerializerService());
                            break;
                        }
                    case "2":
                        {
                            doctorAppointment = new DoctorAppointment(Constants.JsonAppSettingsPath, new JsonDataSerializerService());
                            break; 
                        }

                    default:
                        Console.WriteLine("Wrong choice...");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            doctorAppointment?.Menu();
        }
    }
}