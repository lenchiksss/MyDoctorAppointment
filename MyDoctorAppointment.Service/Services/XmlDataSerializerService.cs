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
        public void Serialize<T>(string filePath, T data)
        {
            XmlSerializer result = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                result.Serialize(fs, data);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            XmlSerializer result = new XmlSerializer(typeof(T));

            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return (T)result.Deserialize(stream);
            }
        }
    }
}
