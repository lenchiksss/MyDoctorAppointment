using MyDoctorAppointment.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Service.Services
{
    public class XmlDataSerializerService : ISerializationService
    {
        public T Deserialize<T>(string path)
        {
            XmlSerializer serializer = new(typeof(T));

            using FileStream stream = new(path, FileMode.OpenOrCreate);
            return (T)serializer.Deserialize(stream);
        }

        public void Serialize<T>(string path, T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using FileStream fs = new(path, FileMode.OpenOrCreate);
            serializer.Serialize(fs, data);
        }
    }
}
