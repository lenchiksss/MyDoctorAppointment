using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Interfaces
{
    public interface ISerializationService
    {
        void Serialize<T>(T data, string filePath);

        T Deserialize<T>(string filePath);
    }
}
